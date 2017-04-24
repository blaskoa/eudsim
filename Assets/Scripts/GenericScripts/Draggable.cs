using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragging = false;
    List<Vector3> curentPos = new List<Vector3>();
    Boolean tbitem = false;

    // Mouse and dragged item positions at the start of dragging.
    private Vector2 _mousePos;
    private Vector2 _itemPos;
    private List<Vector2> _itemPoss = new List<Vector2>();

    // Item we're dragging.
    private GameObject _draggingItem;

    //util class for work with toolbar buttons 
    private ToolbarButtonUtils _tbu = new ToolbarButtonUtils();


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();
        
        // Deselect selected item first.
        if (SelectObject.SelectedObjects.Count != 0 && !SelectObject.SelectedObjects.Contains(this.gameObject))
        {
            //deselect item
            GameObject item = GameObject.Find("Canvas");
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
            tbitem = true;
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
            // Newly created component needs to be selected otherwise an error will occur
            SelectObject.AddToSelection(_draggingItem);

            UndoAction undoAction = new UndoAction();
            GUICircuitComponent component = _draggingItem.GetComponent<GUICircuitComponent>();
            List<float> prop = new List<float>();
            prop.Add((float)1.0);
            prop.Add((float)component.GetId());
            prop.Add((float)_draggingItem.gameObject.transform.GetChild(0).GetComponent<Connectable>().GetID());
            prop.Add((float)_draggingItem.gameObject.transform.GetChild(1).GetComponent<Connectable>().GetID());
            CreateDeleteCompChange change = new CreateDeleteCompChange();  
            change.SetPosition(_draggingItem.transform.position);
            change.SetChange(prop);
            change.SetType(_draggingItem.gameObject.GetComponent<GUICircuitComponent>().GetType());
            change.RememberConnectorsToFirst(_draggingItem.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected);
            change.RememberConnectorsToSecond(_draggingItem.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected);
            undoAction.AddChange(change);
            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        else if (this.gameObject.tag == "Node")
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
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 0);
                _draggingItem.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                _draggingItem.transform.GetChild(i).gameObject.layer = 8;
            }

            // Newly created component needs to be selected otherwise an error will occur
            SelectObject.AddToSelection(_draggingItem);
        
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

            // Clear the Properties Window
            script.Clear();

            // Call the script from component that fills the Properties Window
            GUICircuitComponent componentScript = _draggingItem.GetComponent<GUICircuitComponent>();
            componentScript.GetProperties();
        }
        
        _tbu.EnableToolbarButtons();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (_draggingItem != null)
        {           
            if (dragging == false)
            {
                curentPos.Add(_draggingItem.transform.position);
            }
            dragging = true;
            
            //Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
            _draggingItem.transform.position = _itemPos + mouseDiff;
            _draggingItem.transform.position = new Vector3(_draggingItem.transform.position.x, _draggingItem.transform.position.y);
        }
        else if (SelectObject.SelectedObjects.Count > 1)
        {
            //Moving the item with the mouse.
            Vector2 mouseDiff = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position) - _mousePos;
            int i = 0;
            
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                if (dragging == false)
                {
                    curentPos.Add(objectSelected.transform.position);
                }
                objectSelected.transform.position = _itemPoss[i] + mouseDiff;
                i++;
            }
            dragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }

        if (_draggingItem != null)
        {
            dragging = false;

            // Snapping by 0.5f.
            Vector3 finalPos = _draggingItem.transform.position;

            finalPos *= 2;
            finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
            finalPos /= 2;

            _draggingItem.transform.position = finalPos;
            _draggingItem.transform.position = new Vector3(_draggingItem.transform.position.x, _draggingItem.transform.position.y, -6);
            

            if (tbitem == false) {
                List<float> properties = new List<float>();
                properties.Add(_draggingItem.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(curentPos[0][0] - finalPos[0]);
                properties.Add(curentPos[0][1] - finalPos[1]);

                PosChange change = new PosChange(); 
                change.SetChange(properties);

                UndoAction undoAction = new UndoAction();
                undoAction.AddChange(change);

                GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            }
            else
            {
                GUICircuitComponent.globalUndoList.undoList.First.Value.changes[GUICircuitComponent.globalUndoList.undoList.Last.Value.changes.Count-1].position = finalPos;
            }
            curentPos.Clear();
            tbitem = false;

            //checking colision
            Colision();
            _draggingItem.transform.position = new Vector3(_draggingItem.transform.position.x, _draggingItem.transform.position.y, -6);
            _draggingItem = null;
        }
        else if (SelectObject.SelectedObjects.Count > 1)
        {
            dragging = false;
            int i = 0;
            UndoAction undoAction = new UndoAction();

            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                Vector3 finalPos = objectSelected.transform.position;

                finalPos *= 2;
                finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
                finalPos /= 2;

                objectSelected.transform.position = finalPos;
                List<float> properties = new List<float>();
                properties.Add(objectSelected.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(curentPos[i][0] - finalPos[0]);
                properties.Add(curentPos[i][1] - finalPos[1]);

                PosChange change = new PosChange(); 
                change.SetChange(properties);
                undoAction.AddChange(change);
                i++;

                //checking colision
                Colision();
            }
            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            curentPos.Clear();
        }

        _itemPoss.Clear();

        //transform position of each lines in scene
        GameObject line = GameObject.Find("Line(Clone)");
        if (line != null)
        {
            line.GetComponent<Line>().TransformLines();
        }
    }

    //checking colision
    public void Colision()
    {
        //all objects on scene
        GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("ActiveItem");
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("ActiveNode");

        //merge two arrays to one
        GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).ToArray();
        
        // Save starting positions of each selected GameObject
        Vector2 topLeftMost = new Vector2(
            SelectObject.SelectedObjects[0].transform.position.x - 1f,
            SelectObject.SelectedObjects[0].transform.position.y + 1f
        );
        Vector3[] startPositions = new Vector3[SelectObject.SelectedObjects.Count];
        for (int index = 0; index < SelectObject.SelectedObjects.Count; index++)
        {
            startPositions[index] = new Vector3(
                SelectObject.SelectedObjects[index].transform.position.x,
                SelectObject.SelectedObjects[index].transform.position.y,
                -6
            );
            // Get the leftmost coordinates of the selection
            if (SelectObject.SelectedObjects[index].transform.position.x < topLeftMost.x)
            {
                topLeftMost.x = SelectObject.SelectedObjects[index].transform.position.x - 1f;
            }
            if (SelectObject.SelectedObjects[index].transform.position.y > topLeftMost.y)
            {
                topLeftMost.y = SelectObject.SelectedObjects[index].transform.position.y + 1f;
            }
        }

        // Get all GameObjects that are to the right and down of the top-left most coordinate of the whole selection
        ArrayList potentialColliders = new ArrayList();
        foreach (GameObject go in gameObjects)
        {
            if (!SelectObject.SelectedObjects.Contains(go))
            {
                if (go.transform.position.x >= topLeftMost.x &&
                    go.transform.position.y <= topLeftMost.y
                    )
                {
                    potentialColliders.Add(go);
                }
            }
        }

        // Place GameObjects
        int placed = 0;
        int i = 0;

        //find colision and drag to clear space on grid
        while (placed != SelectObject.SelectedObjects.Count)
        {
            for (int j = i; j >= 0; j--)
            {
                // Set new position for every selected object
                for (var index = 0; index < SelectObject.SelectedObjects.Count; index++)
                {
                    SelectObject.SelectedObjects[index].transform.position = startPositions[index] + new Vector3(j * 1.5f, j * 1.5f - i * 1.5f, 0f);
                }

                placed = 0;
                foreach (GameObject objectSelected in SelectObject.SelectedObjects)
                {
                    // Check if the GameObject is colliding with any of the existing GameObjects
                    bool touching = false;
                    foreach (GameObject potentialCollider in potentialColliders)
                    {
                        if (objectSelected.GetComponent<BoxCollider2D>()
                            .bounds.Intersects(potentialCollider.GetComponent<BoxCollider2D>().bounds))
                        {
                            touching = true;
                            break;
                        }
                    }

                    // Stop the algorithm if a collision was detected
                    if (!touching)
                    {
                        placed++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (placed == SelectObject.SelectedObjects.Count)
                {
                    break;
                }
            }
            i++;
        }
    }
}
