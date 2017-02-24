using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectObject : MonoBehaviour, IPointerClickHandler
{
    
    // Global variable to allow only one selected item.
    public GameObject SelectionBox;
    public static List<GameObject> SelectedObjects = new List<GameObject>();

    //util class for work with toolbar buttons 
    private ToolbarButtonUtils tbu = new ToolbarButtonUtils();

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

        if (this.gameObject.tag == "ToolboxItem" || this.gameObject.tag == "Node")
        {
            return;
        }

        // Deselect selected item first.
        if (SelectedObjects.Count != 0)
        {           
            foreach (GameObject objectSelected in SelectedObjects)
            {
                objectSelected.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            }
            SelectedObjects.Clear();
        }

        // Deselect line
        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
            script.Clear();
        }

        // Select new object.        
        SelectedObjects.Add(this.gameObject);
        SelectionBox.GetComponent<SpriteRenderer>().enabled = true;
		
		tbu.EnableToolbarButtons();

        // Clear the Properties Window
        script.Clear();
       
        // Call the script from component that fills the Properties Window
        if (SelectedObjects.Count == 1)
        {
            GUICircuitComponent componentScript = SelectedObjects[0].GetComponent<GUICircuitComponent>();
            componentScript.GetProperties();
        }
    }
}
