using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SelectObject : MonoBehaviour {
    
    // Global variable to allow only one selected item.
    public static GameObject SelectedObject;
    public GameObject SelectionBox;

    // Initialization: Making object and SelectionBox the same size.
    void Start ()
    {
        SelectionBox.transform.position = this.transform.position;
        SelectionBox.transform.localScale = this.transform.localScale;
        SelectionBox.GetComponent<SpriteRenderer>().enabled = false;
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
        if (SelectedObject != null)
        {
            SelectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectedObject = this.gameObject;
        }

        // Select new object.
        SelectedObject = this.gameObject;
        SelectionBox.GetComponent<SpriteRenderer>().enabled = true;
    }
}
