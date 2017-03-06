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
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        if (this.gameObject.tag == "ToolboxItem" || this.gameObject.tag == "Node" )
        {
            return;
        }

        // Deselect selected item first.
        if (SelectedObject != null)
        {
            SelectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectedObject = this.gameObject;
        }

        // Deselect line
        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
            script.Clear();
        }

        // Select new object.
        SelectedObject = this.gameObject;
        SelectionBox.GetComponent<SpriteRenderer>().enabled = true;
		
		// Enable buttons for component manipulation
        GameObject rotateLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rotateRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rotateLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        rotateRightButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;

        // Clear the Properties Window
        script.Clear();

        // Call the script from component that fills the Properties Window
        GUICircuitComponent componentScript = SelectedObject.GetComponent<GUICircuitComponent>();
        if (componentScript.CompareTag("ActiveItem"))
        {
            componentScript.GetProperties();
        }
    }
}
