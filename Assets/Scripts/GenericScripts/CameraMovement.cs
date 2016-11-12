using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    // Mouse and dragged item positions at the start of dragging.
    private Vector2 _mousePos;
    private Vector3 _cameraPos;

    // Item we're dragging.
    [SerializeField] private Camera _mainCamera;

    // Update is called once per frame
    void Update () {

        // Getting initial positions.
        if (Input.GetMouseButtonDown(2))
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _cameraPos = _mainCamera.transform.position;
        }

        // Moving the camera.
        if (Input.GetMouseButton(2))
        {
            // Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _mousePos;
            // Reducing the camera movement speed.
            mouseDiff /= 1.5f;
            Vector3 newPos;
            newPos.x = _cameraPos.x + mouseDiff.x;
            newPos.y = _cameraPos.y + mouseDiff.y;
            newPos.z = _cameraPos.z;
            _mainCamera.transform.position = newPos;
        }
    }
}
