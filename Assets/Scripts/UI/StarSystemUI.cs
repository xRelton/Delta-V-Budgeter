using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StarSystemUI : MonoBehaviour {
    private UIScreen OrbitScreen { get; set; }
    private StarSystemController SystemController;
    private UIComponents UI;
    private GameObject[] ChildrenLines;
    void Start() {
        UI = GetComponent<UIComponents>();
        SystemController = GetComponent<StarSystemController>();
        SystemController.CurrentPos = new List<int> { 3, 3 };
        ChildrenLines = new GameObject[2];
        UI.LoadResources();
        FormSystemMap(transform);
        FormOrbitUI(transform);
    }
    private void FormSystemMap(Transform canvas) {
        SystemController.Sol = GetComponent<SQLiteDataController>().EnactDatabase();
        for (int i = 0; i < SystemController.Sol.Length; i++) {
            int a = i;
            if (SystemController.Sol[i].Name == "Earth Capture / Escape") {
                SystemController.Sol[i].Button = UI.NewButton(40, 280 - i * 40, canvas, SystemController.Sol[i].Name);
            } else {
                SystemController.Sol[i].Button = UI.NewButton(0, 280 - i * 40, canvas, SystemController.Sol[i].Name);
            }
            SystemController.Sol[i].Button.GetComponent<Button>().onClick.AddListener(() => PosClick(a));
            for (int j = 0; j < SystemController.Sol[i].SubPositions.Length; j++) {
                int b = j;
                SystemPosition CurrentPosition = SystemController.Sol[i].SubPositions[j];
                SystemController.Sol[i].SubPositions[j].Button = UI.NewButton((j + 1) * (((2 * (i % 2)) - 1) * 170), 0, SystemController.Sol[i].Button.transform, CurrentPosition.Name);
                if (CurrentPosition.SubPositions.Length > 0) {
                    for (int k = 0; k < CurrentPosition.SubPositions.Length; k++) {
                        int c = k;
                        CurrentPosition.SubPositions[k].Button = UI.NewButton(0, -(k + 1) * 40, CurrentPosition.Button.transform, CurrentPosition.SubPositions[k].Name);
                        CurrentPosition.SubPositions[k].Button.SetActive(false);
                        CurrentPosition.SubPositions[k].Button.GetComponent<Button>().onClick.AddListener(() => PosClick(a, b, c));
                    }
                    CurrentPosition.Line = UI.NewLine(CurrentPosition.SubPositions.Length, -1, false, CurrentPosition.Button.transform);
                    CurrentPosition.Line.SetActive(false);
                    CurrentPosition.Button.GetComponent<Button>().onClick.AddListener(() => PosClick(a, b, CurrentPosition));
                } else {
                    CurrentPosition.Button.GetComponent<Button>().onClick.AddListener(() => PosClick(a, b));
                }
            }
            SystemController.Sol[i].Line = UI.NewLine(SystemController.Sol[i].SubPositions.Length, ((2 * (i % 2)) - 1), true, SystemController.Sol[i].Button.transform, SystemController.Sol[i].Aero);
        }
        GameObject SpineLine = UI.NewLine(SystemController.Sol.Length, -1, false, SystemController.Sol[0].Button.transform);
        for (int i = 0; i < ChildrenLines.Length; i++) {
            ChildrenLines[i] = UI.NewLine(1, -1, false, SystemController.Sol[0].Button.transform);
            ChildrenLines[i].SetActive(false);
        }
    }
    private void FormOrbitUI(Transform canvas) {
        OrbitScreen = new UIScreen("Orbit Screen", 527, canvas, Instantiate(Resources.Load("BaseSquare")));
        OrbitScreen.CanvasObjects.Add(UI.NewText(0, 200, OrbitScreen.UICanvas.transform, "PosName", SystemController.GetSystemPosition().Name));
        OrbitScreen.CanvasObjects.Add(UI.NewText(0, -220, OrbitScreen.UICanvas.transform, "DVCount", "Delta V not available"));
        OrbitScreen.CanvasObjects.Add(UI.NewButton(0, 20, OrbitScreen.UICanvas.transform, "Select Departure"));
        OrbitScreen.CanvasObjects.Add(UI.NewButton(0, -20, OrbitScreen.UICanvas.transform, "Select Destination"));
        OrbitScreen.CanvasObjects.Add(UI.NewButton(0, -50, OrbitScreen.UICanvas.transform, "Swap"));
        OrbitScreen.CanvasObjects.Add(UI.NewButton(0, -100, OrbitScreen.UICanvas.transform, "Aerobrake [  ]"));
        OrbitScreen.CanvasObjects.Add(UI.NewButton(0, -140, OrbitScreen.UICanvas.transform, "Roundtrip [  ]"));
        OrbitScreen.CanvasObjects[2].GetComponent<Button>().onClick.AddListener(() => SelectClick(0));
        OrbitScreen.CanvasObjects[3].GetComponent<Button>().onClick.AddListener(() => SelectClick(1));
        OrbitScreen.CanvasObjects[4].GetComponent<Button>().onClick.AddListener(() => SwapClick());
        OrbitScreen.CanvasObjects[5].GetComponent<Button>().onClick.AddListener(() => AeroClick());
        OrbitScreen.CanvasObjects[6].GetComponent<Button>().onClick.AddListener(() => RoundClick());
        OrbitScreen.CanvasObjects[4].GetComponent<RectTransform>().sizeDelta -= new Vector2(100, 10);
        OrbitScreen.CanvasObjects[5].GetComponent<RectTransform>().sizeDelta -= new Vector2(40, 0);
        OrbitScreen.CanvasObjects[6].GetComponent<RectTransform>().sizeDelta -= new Vector2(40, 0);
    }
    private void SwapClick() {
        List<Vector4> buttonColours = new List<Vector4> {
            new Vector4(237, 115, 88, 255) / 255, // Changes departure position to red as starboard is red (on depature)
            new Vector4(130, 237, 88, 255) / 255 // Changes destination position to green as starboard is green (on entry)
        };
        List<int> Departure = SystemController.Route[0];
        List<int> Destination = SystemController.Route[1];
        SystemController.Route[0] = Destination;
        SystemController.Route[1] = Departure;
        SystemController.GetSystemPosition(Departure).Button.GetComponent<Image>().color = buttonColours[1];
        SystemController.GetSystemPosition(Destination).Button.GetComponent<Image>().color = buttonColours[0];
        OrbitScreen.CanvasObjects[1].GetComponent<Text>().text = "Delta V: " + SystemController.GetRouteDV();
    }
    private void AeroClick() {
        if (SystemController.Aerobrake) {
            OrbitScreen.CanvasObjects[5].GetComponentInChildren<Text>().text = "Aerobrake [  ]";
        } else {
            OrbitScreen.CanvasObjects[5].GetComponentInChildren<Text>().text = "Aerobrake [X]";
        }
        SystemController.Aerobrake = !SystemController.Aerobrake;
        OrbitScreen.CanvasObjects[1].GetComponent<Text>().text = "Delta V: " + SystemController.GetRouteDV();
    }
    private void RoundClick() {
        if (SystemController.Roundtrip) {
            OrbitScreen.CanvasObjects[6].GetComponentInChildren<Text>().text = "Roundtrip [  ]";
        } else {
            OrbitScreen.CanvasObjects[6].GetComponentInChildren<Text>().text = "Roundtrip [X]";
        }
        SystemController.Roundtrip = !SystemController.Roundtrip;
        OrbitScreen.CanvasObjects[1].GetComponent<Text>().text = "Delta V: " + SystemController.GetRouteDV();
    }
    private void PosClick(int a, int b = -1, int c = -1) {
        List<int> posVal = SystemController.GetPosVal(a, b, c);
        OrbitScreen.CanvasObjects[0].GetComponent<Text>().text = SystemController.GetSystemPosition(posVal).Name;
        SystemController.CurrentPos = posVal;
    }
    private void PosClick(int a, int b, SystemPosition ParentTransfer) { // Activate Sub Buttons
        PosClick(a, b);
        bool activate = !ParentTransfer.Line.activeSelf;
        ParentTransfer.Line.SetActive(activate);
        for (int i = 0; i < ParentTransfer.SubPositions.Length; i++) {
            bool posSelected = false;
            foreach (List<int> pos in SystemController.Route) {
                if (pos != null && ParentTransfer.SubPositions[i] == SystemController.GetSystemPosition(pos)) {
                    ParentTransfer.SubPositions[i].Button.transform.position = ParentTransfer.Button.transform.position;
                    if (activate == false) {
                        ParentTransfer.SubPositions[i].Button.transform.position -= new Vector3(0, 40);
                        posSelected = true;
                    } else {
                        ParentTransfer.SubPositions[i].Button.transform.position -= new Vector3(0, (i + 1) * 40);
                    }
                }
            }
            ParentTransfer.SubPositions[i].Button.SetActive(posSelected || activate);
        }
    }
    private void SelectClick(int selectNum) {
        List<Vector4> buttonColours = new List<Vector4> {
            new Vector4(237, 115, 88, 255) / 255, // Changes departure position to red as starboard is red (on depature)
            new Vector4(130, 237, 88, 255) / 255 // Changes destination position to green as starboard is green (on entry)
        };
        List<int> OldPos = SystemController.Route[selectNum];
        if (OldPos != null) {
            if (SystemController.Route[-(selectNum - 1)] == null || !SystemController.Route[0].SequenceEqual(SystemController.Route[1])) {
                SystemController.GetSystemPosition(OldPos).Button.GetComponent<Image>().color = new Color(1, 1, 1); // Changes old position back to white
                if (OldPos.Count == 3) {
                    ChildrenLines[selectNum].SetActive(false);
                    SystemPosition OldPosParent = SystemController.GetSystemPosition(OldPos.Take(2).ToList());
                    if (!OldPosParent.Line.activeSelf) {
                        SystemController.GetSystemPosition(OldPos).Button.SetActive(false);
                    }
                    SystemController.GetSystemPosition(OldPos).Button.transform.position = OldPosParent.Button.transform.position + new Vector3(0, -(OldPos[2] + 1) * 40);
                }
            } else if (!OldPos.SequenceEqual(SystemController.CurrentPos)) { // if OldPosition is different to NewPosition
                SystemController.GetSystemPosition(OldPos).Button.GetComponent<Image>().color = buttonColours[-(selectNum - 1)];
            }
        }

        SystemController.Route[selectNum] = SystemController.CurrentPos;

        SystemController.GetSystemPosition().Button.GetComponent<Image>().color = buttonColours[selectNum]; 
        OrbitScreen.CanvasObjects[selectNum+2].GetComponent<Image>().color = buttonColours[selectNum];

        if (SystemController.Route[-(selectNum - 1)] != null && SystemController.Route[0].SequenceEqual(SystemController.Route[1])) { // Orange thing
            SystemController.GetSystemPosition().Button.GetComponent<Image>().color = new Vector4(255, 191, 0, 255) / 255;
        }

        if (SystemController.Route[selectNum].Count == 3) {
            ChildrenLines[selectNum].SetActive(true);
            ChildrenLines[selectNum].transform.position = SystemController.GetSystemPosition(SystemController.Route[selectNum].Take(2).ToList()).Button.transform.position + new Vector3(0, -16);
        }
        if (SystemController.Route[-(selectNum - 1)] != null) {
            OrbitScreen.CanvasObjects[1].GetComponent<Text>().text = "Delta V: " + SystemController.GetRouteDV();
        }
    }
}