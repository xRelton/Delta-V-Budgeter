using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSystemController : MonoBehaviour {
    public UIScreen OrbitScreen { get; set; }
    public SystemPosition[] Sol { get; set; }
    private List<int> CurrentPos;
    private GameObject BaseButton;
    private GameObject BaseText;
    private GameObject BaseSquare;
    // Start is called before the first frame update
    void Start() {
        CurrentPos = new List<int> { 0 };
        BaseButton = (GameObject)Resources.Load("BaseButton");
        BaseText = (GameObject)Resources.Load("BaseText");
        BaseSquare = (GameObject)Resources.Load("BaseSquare");
        Transform canvas = GameObject.Find("Canvas").transform;
        FormSystemMap(canvas);
        FormOrbitUI(canvas);
    }
    public SystemPosition GetSystemPosition(List<int> posVal) {
        SystemPosition Pos = Sol[posVal[0]];
        for (int i = 1; i < posVal.Count; i++) {
            Pos = Pos.SubPositions[posVal[i]];
        }
        return Pos;
    }
    private void FormSystemMap(Transform canvas) {
        Sol = gameObject.GetComponent<StarSystemData>().SolarSystem();
        for (int i = 0; i < Sol.Length; i++) {
            if (Sol[i].Name == "Earth Capture / Escape") {
                Sol[i].Button = NewButton(40, 280 - i * 40, canvas, Sol[i].Name);
            } else {
                Sol[i].Button = NewButton(0, 280 - i * 40, canvas, Sol[i].Name);
            }
            Sol[i].Button.GetComponent<Button>().onClick.AddListener(() => PosClicked(new List<int> { i }));
            for (int j = 0; j < Sol[i].SubPositions.Length; j++) {
                SystemPosition CurrentPosition = Sol[i].SubPositions[j];
                Sol[i].SubPositions[j].Button = NewButton((j + 1) * (((2 * (i % 2)) - 1) * 170), 0, Sol[i].Button.transform, CurrentPosition.Name);
                if (CurrentPosition.SubPositions.Length > 0) {
                    for (int k = 0; k < CurrentPosition.SubPositions.Length; k++) {
                        CurrentPosition.SubPositions[k].Button = NewButton(0, -(k + 1) * 40, CurrentPosition.Button.transform, CurrentPosition.SubPositions[k].Name);
                        CurrentPosition.SubPositions[k].Button.SetActive(false);
                        CurrentPosition.SubPositions[k].Button.GetComponent<Button>().onClick.AddListener(() => PosClicked(new List<int> { i, j, k }));
                    }
                    CurrentPosition.Line = NewLine(CurrentPosition.SubPositions.Length, -1, false, CurrentPosition.Button.transform);
                    CurrentPosition.Line.SetActive(false);
                    CurrentPosition.Button.GetComponent<Button>().onClick.AddListener(() => PosClicked(new List<int> {i, j}, CurrentPosition));
                } else {
                    CurrentPosition.Button.GetComponent<Button>().onClick.AddListener(() => PosClicked(new List<int> { i, j }));
                }
            }
            Sol[i].Line = NewLine(Sol[i].SubPositions.Length, ((2 * (i % 2)) - 1), true, Sol[i].Button.transform);
        }
        GameObject SpineLine = NewLine(Sol.Length, -1, false, Sol[0].Button.transform);
    }
    private void PosClicked(List<int> posVal) {
        CurrentPos = posVal;
    }
    private void PosClicked(List<int> posVal, SystemPosition ParentTransfer) { // Activate Sub Buttons
        PosClicked(posVal);
        bool activate = !ParentTransfer.Line.activeSelf;
        for (int i = 0; i < ParentTransfer.SubPositions.Length; i++) {
            ParentTransfer.SubPositions[i].Button.SetActive(activate);
        }
        ParentTransfer.Line.SetActive(activate);
    }
    private void FormOrbitUI(Transform canvas) {
        OrbitScreen = new UIScreen("Orbit Screen", 527, canvas, Instantiate(Resources.Load("BaseSquare")));
        OrbitScreen.CanvasObjects.Add(NewButton(0, -100, OrbitScreen.UICanvas.transform, "Select"));
        OrbitScreen.CanvasObjects.Add(NewText(0, 100, OrbitScreen.UICanvas.transform, "PosName", GetSystemPosition(CurrentPos).Name));
    }
    private GameObject NewCanvasObjectWithText(GameObject newObject, int x, int y, Transform parent, string name, string content = "") {
        newObject.transform.SetParent(parent, true);
        newObject.transform.position = parent.position + new Vector3(x, y);
        newObject.name = "Button " + name;
        if (content != "") {
            name = content;
        }
        newObject.GetComponentInChildren<Text>().text = name;
        return newObject;
    }
    private GameObject NewButton(int x, int y, Transform parent, string name) {
        return NewCanvasObjectWithText(Instantiate(BaseButton), x, y, parent, name);
    }
    private GameObject NewText(int x, int y, Transform parent, string name, string content) {
        return NewCanvasObjectWithText(Instantiate(BaseText), x, y, parent, name, content);
    }
    private GameObject NewLine(int itemsConnecting, int parity, bool horizontal, Transform parent) {
        GameObject line = Instantiate(BaseSquare);
        line.transform.SetParent(parent, false);
        if (horizontal) {
            line.transform.position = parent.position + new Vector3(parity * itemsConnecting * 71, 0, 0);
            line.transform.localScale = new Vector3(167 * itemsConnecting, 2, 1);
        } else {
            line.transform.position = parent.position + new Vector3(0, parity * itemsConnecting * 16, 0);
            line.transform.localScale = new Vector3(2, 8 + 36 * itemsConnecting, 1);
        }
        line.name = "Line";
        return line;
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
        if (CurrentSub == 0 && Name != "Earth Capture / Escape") {
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

public class UIScreen {
    public UIScreen(string name, int x, Transform canvas, Object objectInstance) {
        ScreenObject = (GameObject)objectInstance;
        ScreenObject.name = name;
        ScreenObject.transform.SetParent(canvas);
        ScreenObject.transform.position = new Vector3(x, 0, 0);
        ScreenObject.transform.localScale = new Vector3(300, 600);
        ScreenObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ScreenObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);

        UICanvas = new GameObject();
        UICanvas.name = "CanvasButtons";
        UICanvas.transform.SetParent(ScreenObject.transform);
        UICanvas.transform.position = ScreenObject.transform.position;
        UICanvas.AddComponent<Canvas>();
        UICanvas.GetComponent<Canvas>().overrideSorting = true;
        UICanvas.GetComponent<Canvas>().sortingOrder = 1;
        UICanvas.AddComponent<CanvasScaler>();
        UICanvas.AddComponent<GraphicRaycaster>();

        CanvasObjects = new List<GameObject>();
    }
    public GameObject ScreenObject { get; }
    public GameObject UICanvas { get; }
    public List<GameObject> CanvasObjects { get; set; }
}