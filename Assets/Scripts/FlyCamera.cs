using UnityEngine;

public class FlyCamera : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        Vector3 PositionChange = new Vector3(); // Changes position of camera
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && gameObject.transform.position.y < 200) {
            PositionChange.y += (gameObject.GetComponent<Camera>().orthographicSize - 150) / 20;
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && gameObject.transform.position.y > -500) {
            PositionChange.y -= (gameObject.GetComponent<Camera>().orthographicSize - 150) / 20;
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && gameObject.transform.position.x > -600) {
            PositionChange.x -= (gameObject.GetComponent<Camera>().orthographicSize - 150) / 20;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && gameObject.transform.position.x < 600) {
            PositionChange.x += (gameObject.GetComponent<Camera>().orthographicSize - 150) / 20;
        }
        gameObject.transform.Translate(PositionChange);

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0) {
            float ZoomChange = 0; // Changes zoom of camera
            if (scroll > 0 && gameObject.GetComponent<Camera>().orthographicSize > 200) {
                ZoomChange = -scroll * 200;
            }
            if (scroll < 0 && gameObject.GetComponent<Camera>().orthographicSize < 400) {
                ZoomChange = -scroll * 200;
            }
            gameObject.GetComponent<Camera>().orthographicSize += ZoomChange;
            gameObject.transform.localScale = new Vector3(gameObject.GetComponent<Camera>().orthographicSize / 300, gameObject.GetComponent<Camera>().orthographicSize / 300, 1);
        }
    }
}
