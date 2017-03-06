using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    private List<Vector2> _itemPoss = new List<Vector2>();

    // Item we're dragging.
    private GameObject _draggingItem;

    //util class for work with toolbar buttons 
    private ToolbarButtonUtils _tbu = new ToolbarButtonUtils();

    private float _step = 0.5f;
    private float _buttonDelay = 0f;
    private float _delay = 0.25f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();
        
        // Deselect selected item first.
        if (SelectObject.SelectedObjects.Count != 0 && !SelectObject.SelectedObjects.Contains(this.gameObject))
        {
            //deselect item
            GameObject item = GameObject.Find("Container");
            item.GetComponent<SelectObject>().DeselectObject();

            // Deselect line
            GameObject line = GameObject.Find("Line(Clone)");
            if (line != null)
            {
                line.GetComponent<Line>().DeselectLine();
            }           
        }
        else if (SelectObject.SelectedObjects.Count > 1 && SelectObject.SelectedObjects.Contains(this.gameObject))
        {
            // Setting starting positions for every selected elements     
            _mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                _itemPoss.Add(objectSelected.transform.position);
            }
        }
        //deselect line
        else 
        {
            GameObject line = GameObject.Find("Line(Clone)");
            if (line != null)
            {
                line.GetComponent<Line>().DeselectLine();
            }
        }

        // Setting starting posiitons.
        _mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        _itemPos = this.gameObject.transform.position;

        // ToolboxItemActive tagged GameObjects are used to generate new instances for the working panel.
        if (this.gameObject.tag == "ToolboxItemActive")
        {
            this.gameObject.tag = "ActiveItem";
            // Awake() function of every script is called when GameObject is instatiated. We need it to be instantiated as ActiveItem.
            _draggingItem = Instantiate(this.gameObject);
            this.gameObject.tag = "ToolboxItemActive";
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
        else if (SelectObject.SelectedObjects.Count == 0 || SelectObject.SelectedObjects.Count == 1 && SelectObject.SelectedObjects[0] == this.gameObject)
        {           
            _draggingItem = this.gameObject;

            if (SelectObject.SelectedObjects.Count == 0)
            {
                // Select new object.        
                SelectObject.SelectedObjects.Add(_draggingItem);
                _draggingItem.GetComponent<SelectObject>().SelectionBox.GetComponent<SpriteRenderer>().enabled = true;
            }
            _tbu.EnableToolbarButtons();

            // Clear the Properties Window
            script.Clear();

            // Call the script from component that fills the Properties Window
            GUICircuitComponent componentScript = _draggingItem.GetComponent<GUICircuitComponent>();
            componentScript.GetProperties();
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
        if (_draggingItem != null)
        {
            //Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
            _draggingItem.transform.position = _itemPos + mouseDiff;
        }
        else if (SelectObject.SelectedObjects.Count > 1)
        {
            //Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
            int i = 0;
            
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                objectSelected.transform.position = _itemPoss[i] + mouseDiff;
                i++;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggingItem != null)
        {
            // Snapping by 0.5f.
            Vector3 finalPos = _draggingItem.transform.position;

            finalPos *= 2;
            finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
            finalPos /= 2;

            _draggingItem.transform.position = finalPos;

            //checking colision
            Colision();
            _draggingItem = null;
        }
        else if (SelectObject.SelectedObjects.Count > 1)
        {
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                Vector3 finalPos = objectSelected.transform.position;

                finalPos *= 2;
                finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
                finalPos /= 2;

                objectSelected.transform.position = finalPos;

                //checking colision
                Colision();
            }           
        }

        _itemPoss.Clear();

        //transform position of each lines in scene
        GameObject line = GameObject.Find("Line(Clone)");
        if (line != null)
        {
            line.GetComponent<Line>().TransformLines();
        }
    }


    void Update()
    {
        // Vector for movement.
        Vector3 movement = new Vector3();

        _decreaseDelay();

        // Check if any object is selected.
        if (SelectObject.SelectedObjects.Count != 0)
        {
            if (SelectObject.SelectedObjects.Contains(this.gameObject))
            { 
                // Check if A is pressed.
                if (HotkeyManager.Instance.CheckHotkey(MoveLeftHotkeyKey))
                {
                    if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'A')
                    {
                        CollisionUtils.Move = true;
                    }
                    CollisionUtils.LastPressed = 'A';
                    movement.x -= _step;
                }

                // Check if D is pressed.
                if (HotkeyManager.Instance.CheckHotkey(MoveRightHotkeyKey))
                {
                    if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'D')
                    {
                        CollisionUtils.Move = true;
                    }
                    CollisionUtils.LastPressed = 'D';
                    movement.x += _step;
                }

                // Check if S is pressed.
                if (HotkeyManager.Instance.CheckHotkey(MoveDownHotkeyKey))
                {
                    if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'S')
                    {
                        CollisionUtils.Move = true;
                    }
                    CollisionUtils.LastPressed = 'S';
                    movement.y -= _step;
                }

                // Check if W is pressed.
                if (HotkeyManager.Instance.CheckHotkey(MoveUpHotkeyKey))
                {
                    if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'W')
                    {
                        CollisionUtils.Move = true;
                    }
                    CollisionUtils.LastPressed = 'W';
                    movement.y += _step;
                }

                // Button delay has passed and some keys were pressed.
                if (_buttonDelay == 0f && (movement.x != 0f || movement.y != 0f))
                {

                    GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("ActiveItem");
                    GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("ActiveNode");

                    //merge two arrays to one
                    GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).ToArray();

                    //check collision with every active item
                    foreach (GameObject go in gameObjects)
                    {
                        if (go != this.gameObject)
                        {
                            if (Math.Abs((go.transform.position.x + this.gameObject.transform.position.x + movement.x) / 2 - go.transform.position.x) <= 0.25 &&
                                Math.Abs((go.transform.position.y + this.gameObject.transform.position.y + movement.y) / 2 - go.transform.position.y) <= 0.25)
                            {                    
                                CollisionUtils.Move = false;
                            }
                        }
                    }

                    //if is possible to move
                    if (CollisionUtils.Move)
                    {
                        this.gameObject.transform.position = this.gameObject.transform.position + movement;
                    }
                    _buttonDelay = _delay;

                    //transform position of each lines in scene
                    GameObject line = GameObject.Find("Line(Clone)");
                    if (line != null)
                    {
                        line.GetComponent<Line>().TransformLines();
                    }
                }              
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

    //checking colision
    public void Colision()
    {
       
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {           
            //position of selected object
            Vector2 startPos = new Vector2(
                objectSelected.transform.position.x,
                objectSelected.transform.position.y
            );

            //all objects on scene
            GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("ActiveItem");
            GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("ActiveNode");

            //merge two arrays to one
            GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).ToArray();
            ArrayList potentialColliders = new ArrayList();
            foreach (GameObject go in gameObjects)
            {
                if (go != objectSelected)
                {
                    if (Math.Abs((go.transform.position.x + startPos.x)/2 - go.transform.position.x) <= 1 &&
                        Math.Abs((go.transform.position.y + startPos.y)/2 - go.transform.position.y) <= 1)
                    {
                        potentialColliders.Add(go);
                    }
                }
            }

            // Place GameObject
            bool placed = false;
            int i = 1;
            while (!placed)
            {
                for (int j = i; j >= 0; j--)
                {
                    // Check if the GameObject is colliding with any of the existing GameObjects
                    bool touching = false;
                    foreach (GameObject go in potentialColliders)
                    {                      
                        if (objectSelected.GetComponent<BoxCollider2D>()
                            .bounds.Intersects(go.GetComponent<BoxCollider2D>().bounds))
                        {
                            touching = true;
                            break;
                        }
                    }
                   
                    // Stop the algorithm
                    if (!touching)
                    {
                        placed = true;
                        break;
                    }

                    //set new position of every selected object
                    foreach (GameObject objectSelected2 in SelectObject.SelectedObjects)
                    {                     
                        objectSelected2.transform.position += new Vector3(j * 1.5f, j * 1.5f - i * 1.5f, 0f);
                    }
                }
                i++;
            }
        }       
    }
}
