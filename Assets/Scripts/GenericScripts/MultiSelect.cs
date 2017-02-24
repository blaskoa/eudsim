using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool _isSelecting = false;
    private Vector3 _mousePosition1;

    //util classes for work with toolbar buttons and drawing rectangle
    private ToolbarButtonUtils _tbu = new ToolbarButtonUtils();
    private MultiSelectUtils _msu = new MultiSelectUtils();

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!_isSelecting)
        {
            return false;
        }

        Bounds viewportBounds = _msu.GetViewportBounds(Camera.main, _mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }


    void OnGUI()
    {
        if (_isSelecting)
        {
            // Create a rect from both mouse positions
            Rect rect = _msu.GetScreenRect(_mousePosition1, Input.mousePosition);      
            _msu.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.2f));
            _msu.DrawScreenRectBorder(rect, 2, Color.white);
        }
    }

    // If we press the left mouse button, save mouse location and begin selection
    public void OnPointerDown(PointerEventData eventData)
    {
        //deselect items first
        DoDeselect();

        //initialize variable
        _isSelecting = true;
        _mousePosition1 = Input.mousePosition;
     }

    // If we let go of the left mouse button, end selection
    public void OnPointerUp(PointerEventData eventData)
    {
        //select every item in selection bounds
        foreach (GameObject selectableObject in GameObject.FindGameObjectsWithTag("ActiveItem"))
        {
            if (IsWithinSelectionBounds(selectableObject.gameObject))
            {
                SelectObject.SelectedObjects.Add(selectableObject);
                selectableObject.GetComponent<SelectObject>().SelectionBox.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        //end of selecting
        _isSelecting = false;

        // Call the script from component that fills the Properties Window
        if (SelectObject.SelectedObjects.Count == 1)
        {
            GUICircuitComponent componentScript = SelectObject.SelectedObjects[0].GetComponent<GUICircuitComponent>();
            componentScript.GetProperties();
        }
       
        //enabling or disabling buttons in toolbar panel
        if (SelectObject.SelectedObjects.Count != 0)
        {          
            _tbu.EnableToolbarButtons();
        }
        else
        {
            _tbu.DisableToolbarButtons();
        }
    }

    public void DoDeselect()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        // Deselect component
        if (SelectObject.SelectedObjects.Count != 0)
        {
            foreach (GameObject objectsSelected in SelectObject.SelectedObjects)
            {
                objectsSelected.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            }
            SelectObject.SelectedObjects.Clear();
            script.Clear();
        }

        // Deselect line
        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
            script.Clear();
        }

        _tbu.DisableToolbarButtons();
    }
}
