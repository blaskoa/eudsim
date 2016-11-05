using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    // Mouse and dragged item positions at the start of dragging.
    private Vector2 mousePos;
    private Vector3 cameraPos;

    // Item we're dragging.
    [SerializeField] private Camera mainCamera;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        // Getting initial positions.
        if (Input.GetMouseButtonDown(2))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cameraPos = mainCamera.transform.position;
        }

        // Moving the camera.
        if (Input.GetMouseButton(2))
        {
            // Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePos;
            // Reducing the camera movement speed.
            mouseDiff /= 1.5f;
            Vector3 newPos;
            newPos.x = cameraPos.x + mouseDiff.x;
            newPos.y = cameraPos.y + mouseDiff.y;
            newPos.z = cameraPos.z;
            mainCamera.transform.position = newPos;
        }
	}
}
