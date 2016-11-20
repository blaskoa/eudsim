using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SelectObject : MonoBehaviour, IPointerClickHandler
{
    
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

    public void OnPointerClick(PointerEventData eventData)
    {
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

        // Clear the Properties Window
        EditObjectProperties.Clear();

        // Call the script from component that fills the Properties Window
        GUICircuitComponent script = SelectedObject.GetComponent<GUICircuitComponent>();
        script.getProperties();
    }
}
