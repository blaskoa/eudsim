using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Mouse and dragged item positions at the start of dragging.
    private Vector2 mousePos;
    private Vector2 itemPos;

    // Item we're dragging.
    private GameObject draggingItem;

    private float step = 0.5f;
    private float buttonDelay = 0f;
    private float delay = 0.25f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setting starting posiitons.
        mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        itemPos = this.transform.position;

        // ToolboxItem tagged GameObjects are used to generate new instances for the working panel.
        if (this.gameObject.tag == "ToolboxItem")
        {
            draggingItem = Instantiate(this.gameObject);
            draggingItem.tag = "Untagged";
        }
        else
        {
            draggingItem = this.gameObject;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Moving the item with the mouse.
        Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - mousePos;
        draggingItem.transform.position = itemPos + mouseDiff;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Snapping by 0.5f.
        Vector3 finalPos = draggingItem.transform.position;

        finalPos *= 2;
        finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
        finalPos /= 2;

        draggingItem.transform.position = finalPos;
    }

    void Update()
    {
        // Vector for movement.
        Vector3 movement = new Vector3();

        decreaseDelay();
        // Check if any object is selected.
        if (SelectObject.selectedObject != null && this.gameObject == SelectObject.selectedObject)
        {
            // Check if A is pressed.
            if (Input.GetKey("a"))
            {
                movement.x -= step;
            }

            // Check if D is pressed.
            if (Input.GetKey("d"))
            {
                movement.x += step;
            }
            
            // Check if S is pressed.
            if (Input.GetKey("s"))
            {
                movement.y -= step;
            }

            // Check if W is pressed.
            if (Input.GetKey("w"))
            {
                movement.y += step;
            }

            // Button delay has passed and some keys were pressed.
            if (buttonDelay == 0f && (movement.x != 0f || movement.y != 0f))
            {
                SelectObject.selectedObject.transform.position += movement;
                buttonDelay = delay;
            }
        }
    }

    // To limit users's spamming and make grid-like movement.
    void decreaseDelay()
    {
        if (buttonDelay > 0f)
        {
            buttonDelay -= Time.deltaTime;
        }
        if (buttonDelay < 0f)
        {
            buttonDelay = 0f;
        }
    }
}
