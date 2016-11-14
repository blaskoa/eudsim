using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

//class for objects which  is used to generate lines between components
public class Connectable : MonoBehaviour
{

    public List<GameObject> Connected;
    public GameObject Obj;
    private CounterForConnect _counter = new CounterForConnect();
    private Line _line;

    // Initialization
    void Start()
    {
        Connected = new List<GameObject>();
    }

    // When left mouse button is pressed...
    void OnMouseDown()
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
                Connector con1 = obj2.GetComponent<Connector>();
                Connector con2 = this.gameObject.GetComponent<Connector>();
                GUICircuit.sim.Connect(con1.DLLConnector, con2.DLLConnector);
                Debug.Log("Vytvoril som connection");
                con1.ConnectedConnectors[con1.countOfConnected] = con2;
                con1.countOfConnected += 1;
                con2.ConnectedConnectors[con2.countOfConnected] = con1;
                con2.countOfConnected += 1;
                Destroy(_line);
                _counter.ResetCount();
            }
            else
            {
                _counter.Decrement();
            }
        }

    }

    public void AddConnected(GameObject connected)
    {
        this.Connected.Add(connected);
    }
}
