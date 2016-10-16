using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Mouse and dragged item positions at the start of dragging.
    private Vector2 mousePos;
    private Vector2 itemPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setting starting posiitons.
        mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        itemPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Moving the item with the mouse.
        Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - mousePos;
        this.transform.position = itemPos + mouseDiff;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
