using UnityEngine;

public class FlyCamera : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        Vector3 NewChange = new Vector3();
        if (gameObject.transform.position.y < 200) {
            if (scroll > 0) {
                NewChange.y += scroll * 200;
            }
            if (Input.GetKey(KeyCode.UpArrow)) {
                NewChange.y += 4;
            }
        }
        if (gameObject.transform.position.y > -500) {
            if (scroll < 0) {
                NewChange.y += scroll * 200;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                NewChange.y -= 4;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > -400) {
            NewChange.x -= 4;
        }
        if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 400) {
            NewChange.x += 4;
        }
        gameObject.transform.Translate(NewChange);
        GameObject.Find("Orbit Screen").transform.Translate(NewChange);
    }
}
