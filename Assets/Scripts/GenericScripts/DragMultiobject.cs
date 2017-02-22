using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMultiobject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private Vector2 _mousePos;
    private List<Vector2> _itemPos = new List<Vector2>();
    private GameObject _draggingItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setting starting positions for every selected elements           
        if (SelectObject.SelectedObjects.Count > 1)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                _itemPos.Add(objectSelected.transform.position);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Moving the item with the mouse.
        Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
        int i = 0;
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {          
            objectSelected.transform.position = _itemPos[i] + mouseDiff;
            i++;
        }
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Snapping by 0.5f.
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {
            Vector3 finalPos = objectSelected.transform.position;

            finalPos *= 2;
            finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
            finalPos /= 2;

            // Check if item is colliding with other item at its final location.
            //finalPos = _checkCollision(_draggingItem, finalPos);

            objectSelected.transform.position = finalPos;
        }
       
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
