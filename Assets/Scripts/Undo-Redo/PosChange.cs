using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PosChange : Change
{
    public List<float> properties = new List<float>();
    public int objId;
    public Vector3 point;

    public override void SetChange(List<float> properties)
    {
        this.properties = properties;
        objId = (int)properties[0];
    }

    public void SetPoint(Vector3 p)
    {
        this.point = p;
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
            Vector3 changePos = new Vector3();
            Vector3 curPos = component.gameObject.transform.position;

            changePos[0] = properties[1];
            changePos[1] = properties[2];
            //changePos[2] = curPos[2];

            component.gameObject.transform.position = component.gameObject.transform.position + changePos;

            if(properties.Count == 4)
            {
                component.gameObject.transform.Rotate(new Vector3(0, 0, properties[3]));
            }

            if (properties.Count == 5)
            {
                  component.gameObject.transform.RotateAround(point, new Vector3(0, 0, 1), properties[3]);
            }
        }
    }

    public override void RedoChange()
    {
        properties[1] = -1 * properties[1];
        properties[2] = -1 * properties[2];

        if (properties.Count > 3)
        {
            properties[3] = -1 * properties[3];
        }

        UndoChange();
        properties[1] = -1 * properties[1];
        properties[2] = -1 * properties[2];

        if (properties.Count > 3)
        {
            properties[3] = -1 * properties[3];
        }
    }
}
