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
            draggingItem.tag = "ActiveItem";
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

    // Check if dragging object is colliding with some other object.
    private Vector3 checkCollision(Vector3 finalPos)
    {
        float xDiff;
        float yDiff;
        // Copy of original position for comparison if recursive call is needed.
        Vector3 originalPos = finalPos;

        GameObject[] activeItems = GameObject.FindGameObjectsWithTag("ActiveItem");
        foreach (GameObject activeItem in activeItems)
        {
            // Not colliding with itself.
            if (activeItem.GetInstanceID() == draggingItem.GetInstanceID())
            {
                continue;
            }

            // Checking if the collision will move item horizontally or vertically.
            xDiff = Mathf.Abs(activeItem.transform.position.x - finalPos.x);
            yDiff = Mathf.Abs(activeItem.transform.position.y - finalPos.y);
            
            if (xDiff > yDiff)
            {
                finalPos = moveX(activeItem, finalPos);
            }
            else
            {
                finalPos = moveY(activeItem, finalPos);
            }
        }
        // If no collision movement was made, end collision checking.
        if (finalPos == originalPos)
        {
            return finalPos;
        }
        // Recursive collision checking.
        else
        {
            return checkCollision(finalPos);
        }
    }
    
    // Moving the item horizontally due to collision.
    private Vector3 moveX(GameObject activeItem, Vector3 finalPos)
    {
        float colliderDiff;
        // Check for collision.
        if (Mathf.Abs(activeItem.transform.position.x - finalPos.x) <
                Mathf.Abs(activeItem.GetComponent<BoxCollider2D>().size.x / 2 + draggingItem.GetComponent<BoxCollider2D>().size.x / 2))
        {
            // Move to the left.
            if (finalPos.x < activeItem.transform.position.x)
            {
                colliderDiff = (finalPos.x + draggingItem.GetComponent<BoxCollider2D>().size.x / 2) - (activeItem.transform.position.x - activeItem.GetComponent<BoxCollider2D>().size.x / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                colliderDiff += 0.5f;
                finalPos.x -= colliderDiff;
            }
            // Move to the right.
            else
            {
                colliderDiff = (activeItem.transform.position.x + activeItem.GetComponent<BoxCollider2D>().size.x / 2) - (finalPos.x - draggingItem.GetComponent<BoxCollider2D>().size.x / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                colliderDiff += 0.5f;
                finalPos.x += colliderDiff;
            }
        }
        return finalPos;
    }

    // Moving the item horizontally due to collision.
    private Vector3 moveY(GameObject activeItem, Vector3 finalPos)
    {
        float colliderDiff;
        // Check for collision.
        if (Mathf.Abs(activeItem.transform.position.y - finalPos.y) <
        Mathf.Abs(activeItem.GetComponent<BoxCollider2D>().size.y / 2 + draggingItem.GetComponent<BoxCollider2D>().size.y / 2))
        {
            // Move item up.
            if (finalPos.y < activeItem.transform.position.y)
            {
                colliderDiff = (finalPos.y + draggingItem.GetComponent<BoxCollider2D>().size.y / 2) - (activeItem.transform.position.y - activeItem.GetComponent<BoxCollider2D>().size.y / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                colliderDiff += 0.5f;
                finalPos.y -= colliderDiff;
            }
            // Move item down.
            else
            {
                colliderDiff = (activeItem.transform.position.y + activeItem.GetComponent<BoxCollider2D>().size.y / 2) - (finalPos.y - draggingItem.GetComponent<BoxCollider2D>().size.y / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                colliderDiff += 0.5f;
                finalPos.y += colliderDiff;
            }
        }
            
        return finalPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Snapping by 0.5f.
        Vector3 finalPos = draggingItem.transform.position;

        finalPos *= 2;
        finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
        finalPos /= 2;

        // Check if item is colliding with other item at its final location.
        finalPos = checkCollision(finalPos);

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
