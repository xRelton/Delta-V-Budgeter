using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarSystemController : MonoBehaviour {
    public SystemPosition[] Sol { get; set; }
    public List<int> CurrentPos;
    public List<int>[] Route;
    public bool Aerobrake;
    public bool Roundtrip;
    // Start is called before the first frame update
    void Start() {
        Route = new List<int>[2];
        Aerobrake = false;
        Roundtrip = false;
    }
    public int GetRouteDV() {
        if (Route[0].SequenceEqual(Route[1])) { return 0; }
        int DV = GetDeltaVDifference(Route) ;
        if (Roundtrip) {
            if (Aerobrake) {
                DV += GetDeltaVDifference(new List<int>[] { Route[1], Route[0] });
            } else {
                DV *= 2;
            }
        }
        return DV;
    }
    //GetSystemPosition(pos.Take(i).ToList()).Aero
 /*
 if ((route[0].Count == take - 1 || route[1].Count == take - 1) && route[0].Count != route[1].Count) { // If route 0 and 1 have a parent-child / grandparent-grandchild relationship
    DV = GetDistToBase(route[(route[0].Count < route[1].Count) ? 1 : 0], take - 1, (Aerobrake && ) ? 0.1f : 1);
} else {
    DV = Math.Abs(GetSystemPosition(route[0].Take(take).ToList()).Dist - GetSystemPosition(route[1].Take(take).ToList()).Dist);
    if (take > 1 && GetSystemPosition(route[0].Take(take - 1).ToList()).Aero) {
        DV *= (Aerobrake && (route[0][take - 1] < route[1][take - 1])) ? 0.1f : 1;
    }
    bool secondTime = false;
    foreach (List<int> pos in route) {
        DV += GetDistToBase(pos, take, (Aerobrake && secondTime) ? 0.1f : 1);
        secondTime = true;
    }
}
*/
    private int GetDVDifferenceSiblings(List<int> posA, List<int> posB) {
        return Math.Abs(GetSystemPosition(posA).Dist - GetSystemPosition(posB).Dist);
    }
    private int GetDeltaVDifference(List<int>[] route, int take = 1) {
        float DV;
        if (route[0].Take(take).SequenceEqual(route[1].Take(take))) { // If route 0 and 1 are the same at the current quantity of items considered in each
            DV = GetDeltaVDifference(route, take + 1);
        } else { // take is the number of positions equal in the routes + 1
            if ((route[0].Count == take - 1 || route[1].Count == take - 1) && route[0].Count != route[1].Count) { // If route 0 and 1 have a parent-child / grandparent-grandchild relationship, one must be a transfer
                if (route[0].Count < route[1].Count) { // If the origin is the parent
                    DV = GetDistToBase(route[1], take - 1, route[0]);
                } else {
                    DV = GetDistToBase(route[0], take - 1);
                }
            } else { // Otherwise (they are siblings or cousins or aunt-niece etc)
                DV = GetDVDifferenceSiblings(route[0].Take(take).ToList(), route[1].Take(take).ToList()); // Add relevant siblings (normal)
                if (Aerobrake && route[0].Take(take - 1).Count() > 0 && GetSystemPosition(route[0].Take(take - 1).ToList()).Aero && route[0][take - 1] < route[1][take - 1]) {
                    //DV = Math.Min(DV, CalculateAerobrakeCost(route[0].Take(take).ToList(), route[1].Take(take).ToList())); // Add relevant siblings (aerobrake if cheaper)
                    DV = CalculateAerobrakeCost(route[0].Take(take).ToList(), route[1].Take(take).ToList(), (route[1].Count > take ? 2:0));
                }
                DV += GetDistToBase(route[0], take); // Add relevant children on both sides, first upwell second downwell
                DV += GetDistToBase(route[1], take, route[0], 1); // Discounts burn down in all aerobraking sections
            }
        }
        return (int)DV;
    }
    /////////////// FOR BRAKETYPE MAKE           ODD mean NO BURN DOWN        >= 2 mean NO BURN UP
    private int CalculateAerobrakeCost(List<int> originPos, List<int> destinationPos, int brakeType = 0) { // Origin must be either parent or sibling of destination
        float DV = 0;
        List<int> transferPos = destinationPos.Take(destinationPos.Count - 1).ToList();
        List<int> capturePos = destinationPos.ToList();
        List<int> orbitPos = destinationPos.ToList();
        capturePos[destinationPos.Count - 1] = 0;
        orbitPos[destinationPos.Count - 1] = GetSystemPosition(transferPos).SubPositions.Length - 2;
        if (GetSystemPosition(transferPos).Name == "Earth Capture / Escape") { capturePos = transferPos; }
        if (GetSystemPosition(orbitPos).Name == "Venus High Atmosphere (50 km)") { orbitPos[destinationPos.Count - 1] -= 1; }
        // Gets cost from origin to atmosphere in segment (not required if departure is from a different parent)
        Debug.Log(brakeType);
        if (brakeType % 2 == 0) {
            if (originPos == transferPos) {
                if (capturePos == transferPos) {
                    DV += GetSystemPosition(orbitPos).Dist / 2;
                } else {
                    DV += GetSystemPosition(capturePos).Dist;
                    DV += GetDVDifferenceSiblings(capturePos, orbitPos) / 2;
                }
            } else {
                DV += GetDVDifferenceSiblings(originPos, orbitPos) / 2;
            }
            // TODO orbit-to-atmosphere cost
        }
        Debug.Log("Down DV: " + DV);
        // Gets cost from atmosphere to destination in segment (not required if being captured by another body i.e. a moon)
        if (brakeType < 2) {
            if (destinationPos[destinationPos.Count - 1] == GetSystemPosition(transferPos).SubPositions.Length - 1) {
                // Surface stuff
            } else {
                // add pre-determined atmosphere-to-orbit cost, maybe calculate as a % of orbit to surface, look up numbers
                DV += GetDVDifferenceSiblings(orbitPos, destinationPos) / 2;
            }
        }
        Debug.Log("Down + Up DV: " + DV);
        return (int)DV;
    }
    private int GetDistToBase(List<int> pos, int baseLevel, List<int> downWellOrigin = null, int brakeType = 0) {
        float DV = 0;
        for (int i = baseLevel; i <= 2; i++) {
            if (pos.Count > i) {
                int DVAddition = GetSystemPosition(pos.Take(i + 1).ToList()).Dist;
                if (Aerobrake && GetSystemPosition(pos.Take(i).ToList()).Aero && downWellOrigin != null) { // Going downwell on aerobraking segment with it enabled
                    //DVAddition = Math.Min(DVAddition, CalculateAerobrakeCost(downWellOrigin, pos.Take(i + 1).ToList(), brakeType + (pos.Count > i + 1 ? 2:0))); // Takes the min value of regular or aerobrake DV
                    DVAddition = CalculateAerobrakeCost(downWellOrigin, pos.Take(i + 1).ToList(), brakeType + (pos.Count > i + 1 ? 2:0));
                }
                DV += DVAddition;
            }
        }
        return (int)DV;
    }
    public List<int> GetPosVal(int a, int b = -1, int c = -1) {
        List<int> posVal = new List<int> { a, b, c };
        posVal.RemoveAll(val => val == -1);
        return posVal;
    }
    public SystemPosition GetSystemPosition(List<int> posVal) {
        SystemPosition Pos = Sol[posVal[0]];
        for (int i = 1; i < posVal.Count; i++) {
            Pos = Pos.SubPositions[posVal[i]];
        }
        return Pos;
    }
    public SystemPosition GetSystemPosition() {
        return (GetSystemPosition(CurrentPos));
    }
}

public class SystemPosition {
    public SystemPosition(string name, int dvFromTransfer, int subs = 0, bool aero = false) {
        Name = name;
        Dist = dvFromTransfer;
        Aero = aero;
        SubPositions = new SystemPosition[subs];
    }
    public string Name { get; }
    public int Dist { get; }
    public bool Aero { get; }
    public SystemPosition[] SubPositions;
    public GameObject Button { get; set; }
    public GameObject Line { get; set; }
}