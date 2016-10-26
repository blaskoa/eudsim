using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    // Zooming parameters.
    private float zoomSize;
    private float maxZoomIn;
    private float maxZoomOut;

    // Use this for initialization
    void Start () {
        zoomSize = 5f;
        maxZoomIn = 2f;
        maxZoomOut = 20f;
    }
	
	// Update is called once per frame
	void Update () {

        // Scrolling up.
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // Checking for max zoom in.
            if (zoomSize > maxZoomIn)
            {
                zoomSize -= 1f;
            }
        }
        // Scrolling down.
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Checking for max zoom out.
            if (zoomSize < maxZoomOut)
            {
                zoomSize += 1f;
            }
        }

        GetComponent<Camera>().orthographicSize = zoomSize;
	}
}
