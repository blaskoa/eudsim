using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

//class for objects which  is used to generate lines between components
public class Connectable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public List<GameObject> Connected;
    public GameObject Obj;
    private CounterForConnect _counter = new CounterForConnect();
    private Line _line;
    private Vector3 _startPos;
    private Vector3 _endPos;

    // Initialization
    public void Start()
    {
        Connected = new List<GameObject>();
        Debug.Log(this.gameObject.transform.position);
        
    }

    /*public void Update()
    {
        Debug.Log("som tu");
    }*/

    // When left mouse button is pressed...
    /*void OnMouseDown()
    {
        //if gameobject is in desktop, not in toolbox
        if (this.gameObject.transform.parent.tag == "ActiveItem")
        {
            _counter.Increment();
            //Debug.Log(_counter.GetCount());
            //find first object to connect
            if (_counter.GetCount() == 1)
            {
                _counter.SetPrevious(this.gameObject);
            }
            //find second object to connect - connecting these two object with line
            //cant connect with himself or with connector belonging to the same component 
            else if (_counter.GetCount() == 2
                && _counter.GetPrevious() != this.gameObject
                && !Connected.Contains(_counter.GetPrevious())
                && _counter.GetPrevious().transform.parent.gameObject != this.gameObject.transform.parent.gameObject)
            {
                Connected.Add(_counter.GetPrevious());
                GameObject obj2 = _counter.GetPrevious();
                obj2.SendMessage("AddConnected", this.gameObject);
                _line = Obj.AddComponent<Line>();
                _line.Begin = _counter.GetPrevious();
                _line.End = this.gameObject;
                Instantiate(Obj);
                Destroy(_line);
                _counter.ResetCount();
            }
            else
            {
                _counter.Decrement();
            }
        }

    }*/

   /* public void AddConnected(GameObject connected)
    {
        this.Connected.Add(connected);
    }*/

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("som tu");
        _startPos = this.gameObject.transform.position;
        _startPos.z = 0;
        _startPos *= 2;
        _startPos = new Vector3(Mathf.Round(_startPos.x), Mathf.Round(_startPos.y));
        _startPos /= 2;

        _endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        _endPos.z = 0;
        _endPos *= 2;
        _endPos = new Vector3(Mathf.Round(_endPos.x), Mathf.Round(_endPos.y));
        _endPos /= 2;


        _line = Obj.AddComponent<Line>();
        _line._startPos = _startPos;
        _line._endPos = _endPos;
        Instantiate(Obj);

       
       /* if (this.gameObject.transform.parent.tag == "ActiveItem")
        {
            _counter.Increment();
            _counter.SetPrevious(this.gameObject);
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        /* Connected.Add(_counter.GetPrevious());
         GameObject obj2 = _counter.GetPrevious();
         obj2.SendMessage("AddConnected", this.gameObject);
         _line = Obj.AddComponent<Line>();
         _line.Begin = _counter.GetPrevious();
         _line.End = this.gameObject;
         Instantiate(Obj);*/

        _endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        _endPos.z = 0;
        _endPos *= 2;
        _endPos = new Vector3(Mathf.Round(_endPos.x), Mathf.Round(_endPos.y));
        _endPos /= 2;

        _line._endPos = _endPos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("connector");
        foreach (GameObject go in objs)
        {
            Vector3 conPos = go.transform.position*2;
            conPos = new Vector3(Mathf.Round(conPos.x), Mathf.Round(conPos.y));
            conPos /= 2;

            if (conPos == _endPos)
            {
                _line._endPos = _endPos;
                break;

            }

        }
        Destroy(_line);
    }
}
