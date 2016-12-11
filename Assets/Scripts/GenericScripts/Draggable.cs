using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts.Hotkeys;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // hotkey keys
    public const string MoveDownHotkeyKey = "MoveDown";
    public const string MoveUpHotkeyKey = "MoveUp";
    public const string MoveLeftHotkeyKey = "MoveLeft";
    public const string MoveRightHotkeyKey = "MoveRight";

    // Mouse and dragged item positions at the start of dragging.
    private Vector2 _mousePos;
    private Vector2 _itemPos;

    // Item we're dragging.
    private GameObject _draggingItem;

    private float _step = 0.5f;
    private float _buttonDelay = 0f;
    private float _delay = 0.25f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setting starting posiitons.
        _mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        _itemPos = this.transform.position;

        // ToolboxItemActive tagged GameObjects are used to generate new instances for the working panel.
        if (this.gameObject.tag == "ToolboxItemActive")
        {
            _draggingItem = Instantiate(this.gameObject);
            _draggingItem.tag = "ActiveItem";
            _draggingItem.layer = 8; //Name of 8th layer is ActiveItem
            _draggingItem.transform.localScale = new Vector3(1,1,0);
            _draggingItem.GetComponent<SpriteRenderer>().enabled = true;
            _draggingItem.GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
            for (int i = 0; i < _draggingItem.transform.childCount; i++)
            {
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                _draggingItem.transform.GetChild(i).gameObject.layer = 8;
            }
        }
        else
        {
            _draggingItem = this.gameObject;
        }

        if (this.gameObject.tag == "Node")
        {
            _draggingItem = Instantiate(this.gameObject);
            _draggingItem.tag = "ActiveNode";
            _draggingItem.layer = 8; //Name of 8th layer is ActiveItem
            _draggingItem.transform.localScale = new Vector3(1, 1, 0);
            _draggingItem.GetComponent<SpriteRenderer>().enabled = true;
            _draggingItem.GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
            for (int i = 0; i < _draggingItem.transform.childCount; i++)
            {
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1,1,0);
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                _draggingItem.transform.GetChild(i).gameObject.layer = 8;
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Moving the item with the mouse.
        Vector2 mouseDiff = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
        _draggingItem.transform.position = _itemPos + mouseDiff;
    }

    // Check if dragging object is colliding with some other object.
    // Collider is the object that has finished moving and is being checked for collisions.
    private Vector3 _checkCollision(GameObject collider, Vector3 finalPos)
    {
        float xDiff;
        float yDiff;
        // Copy of original position for comparison if recursive call is needed.
        Vector3 originalPos = finalPos;

        GameObject[] activeComponents = GameObject.FindGameObjectsWithTag("ActiveItem");
        GameObject[] activeNodes = GameObject.FindGameObjectsWithTag("ActiveNode");

        //merge two arrays to one
        GameObject[] activeItems = activeComponents.Concat(activeNodes).ToArray();
        foreach (GameObject activeItem in activeItems)
        {
            // Not colliding with itself.
            if (activeItem.GetInstanceID() == collider.GetInstanceID())
            {
                continue;
            }

            // Checking if the collision will move item horizontally or vertically.
            xDiff = Mathf.Abs(activeItem.transform.position.x - finalPos.x);
            yDiff = Mathf.Abs(activeItem.transform.position.y - finalPos.y);
            
            if (xDiff > yDiff)
            {
                finalPos = _moveX(collider, activeItem, finalPos);
            }
            else
            {
                finalPos = _moveY(collider, activeItem, finalPos);
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
            return _checkCollision(collider, finalPos);
        }
    }
    
    // Moving the item horizontally due to collision.
    private Vector3 _moveX(GameObject collider, GameObject activeItem, Vector3 finalPos)
    {
        float colliderDiff;
        // Check for collision.
        if (Mathf.Abs(activeItem.transform.position.x - finalPos.x) <
                Mathf.Abs(activeItem.GetComponent<BoxCollider2D>().size.x / 2 + collider.GetComponent<BoxCollider2D>().size.x / 2))
        {
            // Move to the left.
            if (finalPos.x < activeItem.transform.position.x)
            {
                colliderDiff = (finalPos.x + collider.GetComponent<BoxCollider2D>().size.x / 2) - (activeItem.transform.position.x - activeItem.GetComponent<BoxCollider2D>().size.x / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                if (collider.GetComponent<BoxCollider2D>().size.x/2 + activeItem.GetComponent<BoxCollider2D>().size.x/2 >
                    colliderDiff)
                {
                    colliderDiff += 0.5f;
                }
                finalPos.x -= colliderDiff;
            }
            // Move to the right.
            else
            {
                colliderDiff = (activeItem.transform.position.x + activeItem.GetComponent<BoxCollider2D>().size.x / 2) - (finalPos.x - collider.GetComponent<BoxCollider2D>().size.x / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                if (collider.GetComponent<BoxCollider2D>().size.x / 2 + activeItem.GetComponent<BoxCollider2D>().size.x / 2 >
                    colliderDiff)
                {
                    colliderDiff += 0.5f;
                }
                finalPos.x += colliderDiff;
            }
        }
        return finalPos;
    }

    // Moving the item horizontally due to collision.
    private Vector3 _moveY(GameObject collider, GameObject activeItem, Vector3 finalPos)
    {
        float colliderDiff;
        // Check for collision.
        if (Mathf.Abs(activeItem.transform.position.y - finalPos.y) <
        Mathf.Abs(activeItem.GetComponent<BoxCollider2D>().size.y / 2 + collider.GetComponent<BoxCollider2D>().size.y / 2))
        {
            // Move item up.
            if (finalPos.y < activeItem.transform.position.y)
            {
                colliderDiff = (finalPos.y + collider.GetComponent<BoxCollider2D>().size.y / 2) - (activeItem.transform.position.y - activeItem.GetComponent<BoxCollider2D>().size.y / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                if (collider.GetComponent<BoxCollider2D>().size.y/2 + activeItem.GetComponent<BoxCollider2D>().size.y/2 >
                    colliderDiff)
                {
                    colliderDiff += 0.5f;
                }
                finalPos.y -= colliderDiff;
            }
            // Move item down.
            else
            {
                colliderDiff = (activeItem.transform.position.y + activeItem.GetComponent<BoxCollider2D>().size.y / 2) - (finalPos.y - collider.GetComponent<BoxCollider2D>().size.y / 2);
                colliderDiff = Mathf.Round(colliderDiff * 2) / 2;
                if (collider.GetComponent<BoxCollider2D>().size.y / 2 + activeItem.GetComponent<BoxCollider2D>().size.y / 2 >
                    colliderDiff)
                {

                    colliderDiff += 0.5f;
                }
                finalPos.y += colliderDiff;
            }
        }
            
        return finalPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Snapping by 0.5f.
        Vector3 finalPos = _draggingItem.transform.position;

        finalPos *= 2;
        finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
        finalPos /= 2;

        // Check if item is colliding with other item at its final location.
        finalPos = _checkCollision(_draggingItem, finalPos);

        _draggingItem.transform.position = finalPos;
    }

    void Update()
    {
        // Vector for movement.
        Vector3 movement = new Vector3();

        _decreaseDelay();
        // Check if any object is selected.
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            // Check if A is pressed.
            if (HotkeyManager.Instance.CheckHotkey(MoveLeftHotkeyKey))
            {
                movement.x -= _step;
            }

            // Check if D is pressed.
            if (HotkeyManager.Instance.CheckHotkey(MoveRightHotkeyKey))
            {
                movement.x += _step;
            }
            
            // Check if S is pressed.
            if (HotkeyManager.Instance.CheckHotkey(MoveDownHotkeyKey))
            {
                movement.y -= _step;
            }

            // Check if W is pressed.
            if (HotkeyManager.Instance.CheckHotkey(MoveUpHotkeyKey))
            {
                movement.y += _step;
            }

            // Button delay has passed and some keys were pressed.
            if (_buttonDelay == 0f && (movement.x != 0f || movement.y != 0f))
            {
                // Check collision.
                Vector3 finalPos = _checkCollision(SelectObject.SelectedObject, SelectObject.SelectedObject.transform.position + movement);
                SelectObject.SelectedObject.transform.position = finalPos;
                _buttonDelay = _delay;
            }
        }
    }

    // To limit users's spamming and make grid-like movement.
    private void _decreaseDelay()
    {
        if (_buttonDelay > 0f)
        {
            _buttonDelay -= Time.deltaTime;
        }
        if (_buttonDelay < 0f)
        {
            _buttonDelay = 0f;
        }
    }
}
