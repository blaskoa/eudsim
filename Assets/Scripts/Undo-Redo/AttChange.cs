using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AttChange : Change
{
    public List<float> properties = new List<float>();
    public int objId;

    public override void SetChange(List<float> properties)
    {
        objId = (int)properties[0];
        this.properties = properties;
    }

    public override void UndoChange()
    {
        GUICircuitComponent component = null;

        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag.Equals("ActiveItem"))       // bacha lebo pri vytvarani noveho mu musim tiez dat tag active item
            {
                component = obj.GetComponent<GUICircuitComponent>();
                if (component.GetId() == objId)
                {
                    break;
                }
            }
        }

        if (component == null)
        {
            Debug.Log("There is something wrong with Undo/Redo");
        }
        else
        {
            component.SetAllProperties(properties);
        }
    }

    public override void RedoChange()
    {
        for (int i=2;i<properties.Count;i = i+2)
        {
            properties[i] *= -1;
        }

        UndoChange();

        for (int i = 2; i < properties.Count; i = i + 2)
        {
            properties[i] *= -1;
        }
    }
}
