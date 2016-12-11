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

        //deselect component
        if (SelectObject.SelectedObject != null)
        {
            SelectObject.SelectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectObject.SelectedObject = null;

            script.Clear();
        }

        //deselect line
        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
            script.Clear();
        }
        GameObject rLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        rRightButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}