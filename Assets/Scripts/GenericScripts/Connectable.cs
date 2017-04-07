using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//class for objects which  is used to generate lines between components
public class Connectable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static int idConnectors = 0;
    public int idConnector;

    public List<GameObject> Connected;
    public GameObject Obj;
    private Line _line;
    private Vector3 _endPos;

    public void Awake()
    {
        Connected = new List<GameObject>();
        idConnector = idConnectors++;
    }

    public int GetID()
    {
        return idConnector;
    }

    public void SetID(int id)
    {
        idConnector = id;
    }

    // Initialization
    public void Start()
    {
    }

    //add connector object to connector in the ending position to the List of connected connectors 
    public void AddConnected(GameObject connected)
    {
        this.Connected.Add(connected);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //if gameobject is in desktop, not in toolbox
        if (this.gameObject.transform.parent.tag == "ActiveItem" || this.gameObject.transform.parent.tag == "ActiveNode")
        {
            //create line from this object to mouse position
            _line = Obj.AddComponent<Line>();
            _line.Begin = this.gameObject;
            _line.EndPos = this.gameObject.transform.position;
            Instantiate(Obj);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //if gameobject is in desktop, not in toolbox
        if ((this.gameObject.transform.parent.tag == "ActiveItem") || (this.gameObject.transform.parent.tag == "ActiveNode"))
        {
            //update ending position of line to mouse position by dragging
            _endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            _endPos.z = 0;
            _line.EndPos = _endPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject end = null;
        _endPos *= 2;
        _endPos = new Vector3(Mathf.Round(_endPos.x), Mathf.Round(_endPos.y));
        _endPos /= 2;

        //if gameobject is in desktop, not in toolbox
        if (this.gameObject.transform.parent.tag == "ActiveItem" || this.gameObject.transform.parent.tag == "ActiveNode")
        {
            //browse all connestors in scene
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Connector");
            foreach (GameObject go in objs)
            {
                Vector3 conPos = go.transform.position * 2;
                conPos = new Vector3(Mathf.Round(conPos.x), Mathf.Round(conPos.y));
                conPos /= 2;

                //searching connector in mouse position in the end of drag
                if (conPos == _endPos)
                {
                    end = go;
                    break;
                }
            }

            //cant connect with himself or with connector belonging to the same component
            if (end != null
                && end != this.gameObject
                && !Connected.Contains(end)
                && end.transform.parent.gameObject != this.gameObject.transform.parent.gameObject 
                && end.transform.parent.gameObject.tag != "ToolboxItemActive")
            {
                //connecting these two object with line               
                _line.End = end;
                Connected.Add(_line.End);
                _line.End.SendMessage("AddConnected", this.gameObject);
                GameObject newLine = Instantiate(Obj);
                newLine.tag = "ActiveLine";
                newLine.transform.position = new Vector2((_line.Begin.transform.position.x + _line.EndPos.x)/2, 
                    (_line.Begin.transform.position.y + _line.EndPos.y)/2 );

                Connector con1 = _line.End.GetComponent<Connector>();
                Connector con2 = gameObject.GetComponent<Connector>();
                con1.ConnectedConnectors.Add(con2);
                con2.ConnectedConnectors.Add(con1);

                UndoAction undoAction = new UndoAction();

                List<float> prop = new List<float>();
                prop.Add((float)1.0);
                prop.Add((float)_line.End.GetComponent<Connectable>().GetID());
                prop.Add((float)_line.Begin.GetComponent<Connectable>().GetID());

                CreateDeleteLineChange change = DoUndo.dummyObj.AddComponent<CreateDeleteLineChange>();
                change.SetChange(prop);
                undoAction.AddChange(change);
                GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            }

            //destroy all lines which dont connect two connectors except parental Line
            Destroy(_line);
            GameObject[] killEmAll;
            killEmAll = GameObject.FindGameObjectsWithTag("Line");
            foreach (GameObject t in killEmAll)
            {
                if (t.GetComponent<Line>().End == null && t.transform.name != "Line")
                {
                    Destroy(t.gameObject);
                }
                else
                {
                    //set invisible parental Line
                    t.GetComponent<LineRenderer>().SetPosition(1, this.gameObject.transform.position);
                }
            }
        }
    }
}
