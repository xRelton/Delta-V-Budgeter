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
        int DV = GetDeltaVDifference(Route);
        if (Roundtrip) {
            if (Aerobrake) {
                DV += GetDeltaVDifference(new List<int>[] { Route[1], Route[0] });
            } else {
                DV *= 2;
            }
        }
        return DV;
    }
    private int GetDeltaVDifference(List<int>[] route, int take = 1) {
        float DV;
        if (route[0].Take(take).SequenceEqual(route[1].Take(take))) {
            DV = GetDeltaVDifference(route, take + 1);
        } else { // take is the number of positions equal in the routes + 1
            if ((route[0].Count == take - 1 || route[1].Count == take - 1) && route[0].Count != route[1].Count) { // If route 0 and 1 have a parent-child / grandparent-grandchild relationship
                DV = GetDistToBase(route[(route[0].Count < route[1].Count) ? 1 : 0], take - 1, (Aerobrake && (route[0].Count < route[1].Count)) ? 0.1f : 1);
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
        }
        return (int)DV;
    }
    private int GetDistToBase(List<int> pos, int baseLevel, float downWellAero) {
        float DV = 0;
        for (int i = baseLevel; i <= 2; i++) {
            if (pos.Count > i) {
                if (GetSystemPosition(pos.Take(i).ToList()).Aero) {
                    DV += GetSystemPosition(pos.Take(i + 1).ToList()).Dist * downWellAero;
                } else {
                    DV += GetSystemPosition(pos.Take(i + 1).ToList()).Dist;
                }
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
        CurrentSub = 0;
        Name = name;
        Dist = dvFromTransfer;
        Aero = aero;
        SubPositions = new SystemPosition[subs];
        if (subs > 0) {
            if (Name == "Earth") {
                Name += " Capture / Escape";
            } else {
                Name += " Transfer";
            }
        }
    }
    public void AddSub(string name, int distFromPrev, int subs = 0, bool aero = false) {
        if (CurrentSub == 0 && Name != "Earth Capture / Escape" && Name != "Geostationary (35786km)") {
            name += " Capture / Escape";
        } else if (CurrentSub == SubPositions.Length - 2) {
            name += " Orbit";
        } else if (CurrentSub == SubPositions.Length - 1) {
            name += " Surface";
        }
        SubPositions[CurrentSub] = new SystemPosition(name, distFromPrev, subs, aero);
        CurrentSub++;
    }
    private int CurrentSub { get; set; }
    public string Name { get; }
    public int Dist { get; }
    public bool Aero { get; }
    public SystemPosition[] SubPositions;
    public GameObject Button { get; set; }
    public GameObject Line { get; set; }
}