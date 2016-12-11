using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    // Zooming parameters.
    private float _zoomSize;
    private float _maxZoomIn;
    private float _maxZoomOut;

    // Use this for initialization
    void Start () {
        _zoomSize = 5f;
        _maxZoomIn = 2f;
        _maxZoomOut = 20f;
    }
    //Zoom functionality to invoke zoom from button
    public void ZoomIn()
    {
        // Checking for max zoom in.
        if (_zoomSize > _maxZoomIn)
        {
            _zoomSize -= 1f;
        }
    }

    //Zoom functionality to invoke zoom from button
    public void ZoomOut()
    {
        // Checking for max zoom in.
        if (_zoomSize > _maxZoomIn)
        {
            _zoomSize += 1f;
        }
    }

    // Update is called once per frame
    void Update () {

        // Scrolling up.
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // Checking for max zoom in.
            if (_zoomSize > _maxZoomIn)
            {
                _zoomSize -= 1f;
            }
        }
        // Scrolling down.
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Checking for max zoom out.
            if (_zoomSize < _maxZoomOut)
            {
                _zoomSize += 1f;
            }
        }

        GetComponent<Camera>().orthographicSize = _zoomSize;
    }
}
