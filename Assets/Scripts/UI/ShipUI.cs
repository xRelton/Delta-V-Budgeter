using UnityEngine;

public class ShipUI : MonoBehaviour {
    private UIScreen ShipScreen { get; set; }
    private UIComponents UI;
    void Start() {
        UI = GetComponent<UIComponents>();
        FormShipUI(transform);
    }
    private void FormShipUI(Transform canvas) {
        ShipScreen = new UIScreen("Ship Screen", -527, canvas, Instantiate(Resources.Load("BaseSquare")));
        ShipScreen.CanvasObjects.Add(UI.NewText(0, 200, ShipScreen.UICanvas.transform, "ShipName", "Starship"));
    }
}
