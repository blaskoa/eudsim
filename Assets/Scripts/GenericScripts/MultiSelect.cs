using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool _isSelecting = false;
    private Vector3 _mousePosition1;

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!_isSelecting)
        {
            return false;
        }

        var viewportBounds = MultiSelectUtils.GetViewportBounds(Camera.main, _mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }


    void OnGUI()
    {
        if (_isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = MultiSelectUtils.GetScreenRect(_mousePosition1, Input.mousePosition);
            //MultiSelectUtils.DrawScreenRectBorder(rect, 2, Color.green);
            MultiSelectUtils.DrawScreenRect(rect, new Color(0f, 0f, 0f, 0.2f));
            // MultiSelectUtils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
            MultiSelectUtils.DrawScreenRectBorder(rect, 2, Color.white);
        }
    }

    private void EnableButtons()
    {
        // Enable buttons for component manipulation
        GameObject rotateLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rotateRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rotateLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        rotateRightButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    private void DisableButtons()
    {
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

          //enabling or disabling buttons in toolbar panel
            if (SelectObject.SelectedObjects.Count != 0)
            {
                EnableButtons();
            }
            else
            {
                DisableButtons();
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

        DisableButtons();
    }
}
