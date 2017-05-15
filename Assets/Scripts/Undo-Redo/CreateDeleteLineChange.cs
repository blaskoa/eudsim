using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class CreateDeleteLineChange : Change
{
    List<int> connectorsID = new List<int>();
    int destroy;

    public override void SetChange(List<float> properties)  // a dat to k vytvaraniu ciar a k zruseniu ciar
    {
        destroy = (int)properties[0];
        connectorsID.Add((int)properties[1]);
        connectorsID.Add((int)properties[2]);
    }

    public override void UndoChange()
    {
        if (destroy == 1)
        {
            DestroyLine();
        }
        else
        {
            CreateLine();
        }
    }

    public override void RedoChange()
    {
        if (destroy == 1)
        {
            CreateLine();
        }
        else
        {
            DestroyLine();
        }
    }

    void CreateLine()
    {
        Connector con1 = null;
        Connector con2 = null;

        GameObject connector1 = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
        {
            if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == connectorsID[0]))
            {
                connector1 = obj;
                con1 = obj.GetComponent<Connector>();
                break;
            }
        }

        GameObject connector2 = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
        {
            if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == connectorsID[1]))
            {
                connector2 = obj;
                con2 = obj.GetComponent<Connector>();
                break;
            }
        }

        connector1.GetComponent<Connectable>().Connected.Add(connector2);
        connector2.GetComponent<Connectable>().Connected.Add(connector1);

        con1.ConnectedConnectors.Add(con2);
        con2.ConnectedConnectors.Add(con1);

        GameObject ObjOrg = GameObject.Find("Line");
        GameObject Obj = Instantiate(ObjOrg);
        Line _line = Obj.AddComponent<Line>();
        _line.Begin = connector1;
        _line.End = connector2;
        Obj.tag = "ActiveLine";
        SelectObject.SelectedLines.Add(Obj);
    }

    void DestroyLine()
    {
        GameObject connector1 = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
        {
            if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == connectorsID[0]))
            {
                connector1 = obj;
                break;
            }
        }

        GameObject connector2 = null;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Connector"))
        {
            if ((obj.GetComponent<Connectable>() != null) && (obj.GetComponent<Connectable>().GetID() == connectorsID[1]))
            {
                connector2 = obj;
                break;
            }
        }

        connector1.GetComponent<Connectable>().Connected.Remove(connector2);
        connector2.GetComponent<Connectable>().Connected.Remove(connector1);

        connector1.GetComponent<Connector>().ConnectedConnectors.Remove(connector2.GetComponent<Connector>());
        connector2.GetComponent<Connector>().ConnectedConnectors.Remove(connector1.GetComponent<Connector>());
        
        GameObject[] lines = GameObject.FindGameObjectsWithTag("ActiveLine");
        foreach (GameObject currentLine in lines)
        {
            if ((connector1.gameObject == currentLine.GetComponent<Line>().Begin &&
                connector2.gameObject == currentLine.GetComponent<Line>().End)
                ||
                (connector1.gameObject == currentLine.GetComponent<Line>().End &&
                connector2.gameObject == currentLine.GetComponent<Line>().Begin)
                )
            {
                Destroy(currentLine.gameObject);
            }
        }

        SelectObject.SelectedObjects.Clear();
        SelectObject.SelectedLines.Clear();
    }
    
}
