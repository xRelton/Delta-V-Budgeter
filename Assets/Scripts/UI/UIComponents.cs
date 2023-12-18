using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponents : MonoBehaviour {
    private GameObject BaseButton;
    private GameObject BaseText;
    private GameObject BaseSquare;
    private GameObject BaseArrow;
    public void LoadResources() {
        BaseButton = (GameObject)Resources.Load("BaseButton");
        BaseText = (GameObject)Resources.Load("BaseText");
        BaseSquare = (GameObject)Resources.Load("BaseSquare");
        BaseArrow = (GameObject)Resources.Load("BaseArrow");
    }
    private GameObject NewCanvasObjectWithText(GameObject newObject, int x, int y, Transform parent, string name, string content = "") {
        newObject.transform.SetParent(parent.transform);
        newObject.transform.position = parent.position + new Vector3(x, y);
        newObject.name = "Button " + name;
        if (content != "") {
            name = content;
        }
        newObject.GetComponentInChildren<Text>().text = name;
        return newObject;
    }
    public GameObject NewButton(int x, int y, Transform parent, string name) {
        return NewCanvasObjectWithText(Instantiate(BaseButton), x, y, parent, name);
    }
    public GameObject NewText(int x, int y, Transform parent, string name, string content) {
        return NewCanvasObjectWithText(Instantiate(BaseText), x, y, parent, name, content);
    }
    public GameObject NewLine(int itemsConnecting, int parity, bool horizontal, Transform parent, bool aerobrake = false) {
        GameObject line = Instantiate(BaseSquare);
        line.transform.SetParent(parent);
        if (horizontal) {
            line.transform.localPosition = new Vector3(parity * itemsConnecting * 71, 0, 0);
            line.transform.localScale = new Vector3(167 * itemsConnecting, 2, 1);
        } else {
            line.transform.localPosition = new Vector3(0, parity * itemsConnecting * 19, 0);
            line.transform.localScale = new Vector3(2, 8 + 38 * itemsConnecting, 1);
        }
        line.name = "Line";
        if (aerobrake) {
            for (int i = 0; i < itemsConnecting; i++) {
                GameObject arrow = Instantiate(BaseArrow);
                arrow.transform.position = new Vector3(parity * (85 + i * 170), line.transform.position.y, 0);
                if (parent.name == "Button Earth Capture / Escape") {
                    arrow.transform.position += new Vector3(40, 0, 0);
                }
                arrow.transform.SetParent(line.transform);
                arrow.transform.localScale = new Vector3((parity*4)/line.transform.localScale.x, 8/line.transform.localScale.y);
            }
        }
        return line;
    }
}
public class UIScreen {
    public UIScreen(string name, int x, Transform canvas, Object objectInstance) {
        ScreenObject = (GameObject)objectInstance;
        ScreenObject.name = name;
        ScreenObject.transform.SetParent(canvas.GetChild(0));
        ScreenObject.transform.position = new Vector3(x, 0, 0);
        ScreenObject.transform.localScale = new Vector3(300, 600);
        ScreenObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        ScreenObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);

        UICanvas = new GameObject();
        UICanvas.name = "UICanvas";
        UICanvas.transform.SetParent(ScreenObject.transform, false);
        UICanvas.AddComponent<Canvas>();
        UICanvas.GetComponent<Canvas>().overrideSorting = true;
        UICanvas.GetComponent<Canvas>().sortingOrder = 1;
        UICanvas.AddComponent<CanvasScaler>();
        UICanvas.AddComponent<GraphicRaycaster>();

        CanvasObjects = new List<GameObject>();
    }
    private GameObject ScreenObject { get; }
    public GameObject UICanvas { get; }
    public List<GameObject> CanvasObjects { get; set; }
}