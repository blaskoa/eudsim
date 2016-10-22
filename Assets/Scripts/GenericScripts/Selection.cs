using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class Selection : MonoBehaviour {
    
    // Global variable to allow only one selected item.
    static GameObject selectedObject;
    public GameObject selectionBox;

	// Initialization: Making object and SelectionBox the same size.
	void Start () {
        selectionBox.transform.position = this.transform.position;
        selectionBox.transform.localScale = this.transform.localScale;
        selectionBox.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When left mouse button is pressed...
    void OnMouseDown()
    {
        // Don't allow selecting Toolbox Items.
        if (this.gameObject.tag == "ToolboxItem")
        {
            return;
        }

        // Deselect selected item first.
        if (selectedObject != null)
        {
            selectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            selectedObject = this.gameObject;
        }

        // Select new object.
        selectedObject = this.gameObject;
        selectionBox.GetComponent<SpriteRenderer>().enabled = true;
    }
}
