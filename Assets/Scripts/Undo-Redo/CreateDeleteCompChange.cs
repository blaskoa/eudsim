using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class CreateDeleteCompChange : Change
{
    GameObject g;
    Type t;
    int componentID;
    List<int> connectorsID = new List<int>();
    List<float> properties = new List<float>();
    Vector3 position = new Vector3();

    List<int> connectedToFirstConnector = new List<int>();
    List<int> connectedToSecondConnector = new List<int>();

    bool createComponent;
    bool destroyRun;

    public CreateDeleteCompChange()
    {
        CreateDestroyType = true;
        createComponent = true;
        destroyRun = true;
    }

    public override void SetChange(List<float> properties)
    {
        this.properties = properties;
        componentID = (int)properties[1];
        connectorsID.Add((int)properties[2]);
        connectorsID.Add((int)properties[3]);
    }

    public override void UndoChange()
    {
        if (properties[0] == 1.0)
        {
            if(destroyRun == true) 
            {
                DestroyObject();
            }
        }
        else
        {
            CreateObject();
        }
    }

    public override void RedoChange()
    {
        if (properties[0] == 1.0)
        {
            CreateObject();
        }
        else
        {
            if (destroyRun == true)
            {
                DestroyObject();
            }
        }
    }
    
    void CreateObject() 
    {
        if (createComponent == true)
        {
            g = null;
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if ((obj.GetComponent<GUICircuitComponent>() != null) &&(obj.GetComponent<GUICircuitComponent>().GetType() == t))
                {
                    g = (GameObject)Instantiate(obj);
                    break;
                }
            }

            g.tag = "ActiveItem";
            g.layer = 8;
            g.transform.localScale = new Vector3(1, 1, 0);
            g.GetComponent<SpriteRenderer>().enabled = true;
            g.GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
            for (int i = 0; i < g.transform.childCount; i++)
            {
                g.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
                g.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                g.transform.GetChild(i).gameObject.layer = 8;
            }

            g.transform.position = position;

            g.GetComponent<GUICircuitComponent>().Awake();  // toto dat vsade do vsetkych komponentov dat ze OVERRIDE
            g.GetComponent<GUICircuitComponent>().SetId(componentID);

            GameObject inConnector1 = g.transform.GetChild(0).gameObject;
            GameObject inConnector2 = g.transform.GetChild(1).gameObject;

            inConnector1.GetComponent<Connectable>().SetID(connectorsID[0]);
            inConnector2.GetComponent<Connectable>().SetID(connectorsID[1]);

            createComponent = false;

            SelectObject.SelectedObjects.Add(g);
        }
        else
        {
            GameObject inConnector1 = g.transform.GetChild(0).gameObject;

            foreach (int id in connectedToFirstConnector)
            {
                  GameObject outConnector1 = null;
                  foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
                  {
                      if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == id))
                      {
                           outConnector1 = obj;
                           break;
                      }
                  }

                  inConnector1.GetComponent<Connectable>().Connected.Add(outConnector1);
                  outConnector1.GetComponent<Connectable>().Connected.Add(inConnector1);

                  GameObject ObjOrg = GameObject.Find("Line");
                  GameObject Obj = Instantiate(ObjOrg);
                  Line _line = Obj.AddComponent<Line>();
                  _line.Begin = inConnector1;
                  _line.End = outConnector1;
                   Obj.tag = "ActiveLine";
                  SelectObject.SelectedLines.Add(Obj);
           }

           GameObject inConnector2 = g.transform.GetChild(1).gameObject;

           foreach (int id in connectedToSecondConnector)
           {
                  GameObject outConnector2 = null;
                  foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
                  {
                       if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == id))
                        {
                            outConnector2 = obj;
                            break;
                        }
                  }
              
                  inConnector2.GetComponent<Connectable>().Connected.Add(outConnector2);
                  outConnector2.GetComponent<Connectable>().Connected.Add(inConnector2);
                  GameObject ObjOrg = GameObject.Find("Line");
                  GameObject Obj = Instantiate(ObjOrg);
                  Line _line = Obj.AddComponent<Line>();
                   _line.Begin = inConnector2;
                  _line.End = outConnector2;
                  Obj.tag = "ActiveLine";
                  SelectObject.SelectedLines.Add(Obj);
            }

            destroyRun = true;
            createComponent = true;
        }
    }

    void DestroyObject()
    {
        g = null;
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if ((obj.GetComponent<GUICircuitComponent>() != null) && (obj.GetComponent<GUICircuitComponent>().GetId() == componentID))
               {
                    g = obj;
                    break;
                }
        }

        List<GameObject> connected1 =  g.transform.GetChild(0).GetComponent<Connectable>().Connected;
        List<GameObject> connected2 =  g.transform.GetChild(1).GetComponent<Connectable>().Connected;
        
        foreach (GameObject c in connected1)
        {
            c.gameObject.GetComponent<Connectable>()
                .Connected.Remove(g.transform.GetChild(0).gameObject);
        }

        foreach (GameObject c in connected2)
        {
            c.gameObject.GetComponent<Connectable>()
                .Connected.Remove(g.transform.GetChild(1).gameObject);
        }

        GameObject[] lines = GameObject.FindGameObjectsWithTag("ActiveLine");

        foreach (GameObject currentLine in lines)
        {
                if (g.transform.GetChild(0).gameObject ==
                    currentLine.GetComponent<Line>().Begin
                    ||
                    g.transform.GetChild(1).gameObject ==
                    currentLine.GetComponent<Line>().Begin
                    ||
                    g.transform.GetChild(0).gameObject ==
                    currentLine.GetComponent<Line>().End
                    ||
                    g.transform.GetChild(1).gameObject ==
                    currentLine.GetComponent<Line>().End)
                {
                    Destroy(currentLine.gameObject);
                }
        }
        Destroy(g);

        destroyRun = false;

        SelectObject.SelectedObjects.Clear();
        SelectObject.SelectedLines.Clear();
    }

    public void SetType(Type t)
    {
        this.t = t;
    }

    public void RememberConnectorsToFirst(List<GameObject> connected)
    {
        foreach (GameObject c in connected)
        {
            connectedToFirstConnector.Add(c.gameObject.GetComponent<Connectable>().GetID());
        }
    }

    public void RememberConnectorsToSecond(List<GameObject> connected)
    {
        foreach (GameObject c in connected)
        {
            connectedToSecondConnector.Add(c.gameObject.GetComponent<Connectable>().GetID());
        }
    }

    public void SetPosition(Vector3 pos)
    {
        position = pos;
    }
}
