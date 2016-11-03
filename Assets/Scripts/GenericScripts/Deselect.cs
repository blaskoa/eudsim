using UnityEngine;
using System.Collections;

public class Deselect : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // When left mouse button is pressed...
    void OnMouseDown()
    {
        if (SelectObject.selectedObject != null)
        {
            SelectObject.selectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectObject.selectedObject = null;
        }
    }
}
