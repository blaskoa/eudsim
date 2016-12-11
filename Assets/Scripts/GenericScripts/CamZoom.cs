using UnityEngine;
using System.Collections;

public class CamZoom : MonoBehaviour {

    // Zooming parameters.
    private float _zoomSize;
    private float _maxZoomIn;
    private float _maxZoomOut;
    private int _zoomPercent;

    // Use this for initialization
    void Start () {
        _zoomSize = 5f;
        _maxZoomIn = 2f;
        _maxZoomOut = 20f;
        GameObject zoomField = GameObject.Find("ZoomText");
        _zoomPercent = 100;
        zoomField.GetComponent<UnityEngine.UI.Text>().text = _zoomPercent + "%";
    }
    //Zoom functionality to invoke zoom from button
    public void ZoomIn()
    {
        // Checking for max zoom in.
        if (_zoomSize > _maxZoomIn)
        {
            _zoomSize -= 1f;
            _zoomPercent += 25;
            GameObject zoomField = GameObject.Find("ZoomText");
            zoomField.GetComponent<UnityEngine.UI.Text>().text = _zoomPercent + "%";
        }
    }

    //Zoom functionality to invoke zoom from button
    public void ZoomOut()
    {
        // Checking for max zoom in.
        if (_zoomSize < _maxZoomOut)
        {
            _zoomSize += 1f;
            _zoomPercent -= 25;
            GameObject zoomField = GameObject.Find("ZoomText");
            zoomField.GetComponent<UnityEngine.UI.Text>().text = _zoomPercent + "%";
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
                _zoomPercent += 25;
                GameObject zoomField = GameObject.Find("ZoomText");
                zoomField.GetComponent<UnityEngine.UI.Text>().text = _zoomPercent + "%";
                
            }
        }
        // Scrolling down.
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Checking for max zoom out.
            if (_zoomSize < _maxZoomOut)
            {
                _zoomSize += 1f;
                _zoomPercent -= 25;
                GameObject zoomField = GameObject.Find("ZoomText");
                zoomField.GetComponent<UnityEngine.UI.Text>().text = _zoomPercent + "%";
            }
        }

        GetComponent<Camera>().orthographicSize = _zoomSize;
    }
}
