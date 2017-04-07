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

        connector1.GetComponent<Connectable>().Connected.Add(connector2);
        connector2.GetComponent<Connectable>().Connected.Add(connector1);

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

        List<GameObject> connected1 = connector1.GetComponent<Connectable>().Connected;
        List<GameObject> connected2 = connector2.GetComponent<Connectable>().Connected;

        foreach (GameObject c in connected1)
        {
            c.gameObject.GetComponent<Connectable>().Connected.Remove(connector2.gameObject);
        }

        foreach (GameObject c in connected2)
        {
            c.gameObject.GetComponent<Connectable>().Connected.Remove(connector1.gameObject);
        }

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
