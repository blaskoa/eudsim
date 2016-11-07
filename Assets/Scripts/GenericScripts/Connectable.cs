using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using System.Collections.Generic;

public class Connectable : MonoBehaviour
{

    public List<GameObject> connected;
    public GameObject obj;
    private CounterForConnect counter = new CounterForConnect();
    private Line line;

    // Initialization
    void Start()
    {
        connected = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When left mouse button is pressed...
    void OnMouseDown()
    {
        counter.increment();
        Debug.Log(counter.getCount());
        if (counter.getCount() == 1)
        {
            counter.setPrevious(this.gameObject);
        }
        else if (counter.getCount() == 2
            && counter.getPrevious() != this.gameObject
            && !connected.Contains(counter.getPrevious())
            && counter.getPrevious().transform.parent.gameObject != this.gameObject.transform.parent.gameObject)
        {
            connected.Add(counter.getPrevious());
            GameObject obj2 = GameObject.Find(counter.getPrevious().name);
            obj2.SendMessage("addConnected", this.gameObject);
            line = obj.AddComponent<Line>();
            line.start = counter.getPrevious();
            line.end = this.gameObject;
            Instantiate(obj);
            Destroy(line);
            counter.resetCount();
        }
        else
        {
            counter.decrement();
        }

    }

    public void addConnected(GameObject connected)
    {
        this.connected.Add(connected);
    }
}
