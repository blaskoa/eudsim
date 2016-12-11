using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Deselect : MonoBehaviour, IPointerClickHandler
{
    // When left mouse button is pressed...
    public void OnPointerClick(PointerEventData eventData)
    {
        DoDeselect();
    }

    public void DoDeselect()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        // Deselect component
        if (SelectObject.SelectedObject != null)
        {
            SelectObject.SelectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectObject.SelectedObject = null;

            script.Clear();
        }

        // Deselect line
        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
            script.Clear();
        }
		
		// Disable buttons for component manipulation
        GameObject rotateLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rotateRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rotateLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        rotateRightButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}