using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Hotkeys;
using System;

public class RotateMultiObject : MonoBehaviour {

    public const string RotateLeftHotkeyKey = "RotateLeft";
    public const string RotateRightHotkeyKey = "RotateRight";
    

    public void RotateMultiObjectClockWise()
    {
        Vector3 point = CalculateCenterPoint();

        if (SelectObject.SelectedObjects.Count > 1)
        {
            UndoAction undoAction = new UndoAction();
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                Vector3 curentPos = objectSelected.transform.position;

                objectSelected.transform.RotateAround(point, new Vector3(0, 0, 1), -90);

                Vector3 finalPos = objectSelected.transform.position;

                finalPos *= 2;
                finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
                finalPos /= 2;

                objectSelected.transform.position = finalPos;

                List<float> properties = new List<float>();
                properties.Add(objectSelected.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(0);
                properties.Add(0);
                properties.Add(+90);
                properties.Add(0);

                PosChange change = DoUndo.dummyObj.AddComponent<PosChange>();
                change.SetChange(properties);
                change.SetPoint(point);
                
                undoAction.AddChange(change);
            }

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);

            //checking colision
            GameObject.Find("Container").GetComponent<Draggable>().Colision();

            //transform position of each lines in scene
            GameObject line = GameObject.Find("Line(Clone)");
            if (line != null)
            {
                line.GetComponent<Line>().TransformLines();
            }
        }
    }

    public void RotateMultiObjectCounterClockWise()
    {
        Vector3 point = CalculateCenterPoint();

        if (SelectObject.SelectedObjects.Count > 1)
        {
            UndoAction undoAction = new UndoAction();
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
               Vector3 curentPos = objectSelected.transform.position;

                objectSelected.transform.RotateAround(point, new Vector3(0, 0, 1), +90);

                Vector3 finalPos = objectSelected.transform.position;

                finalPos *= 2;
                finalPos = new Vector3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y));
                finalPos /= 2;

                objectSelected.transform.position = finalPos;

                List<float> properties = new List<float>();
                properties.Add(objectSelected.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(0);
                properties.Add(0);
                properties.Add(-90);
                properties.Add(0);

                PosChange change = DoUndo.dummyObj.AddComponent<PosChange>();
                change.SetChange(properties);
                change.SetPoint(point);

                undoAction.AddChange(change);
            }
            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            //checking colision
            GameObject.Find("Container").GetComponent<Draggable>().Colision();

            //transform position of each lines in scene
            GameObject line = GameObject.Find("Line(Clone)");
            if (line != null)
            {
                line.GetComponent<Line>().TransformLines();
            }
        }
    }

    public Vector3 CalculateCenterPoint()
    {
        if (SelectObject.SelectedObjects.Count > 1)
        {
            //calculate the point in center of selected items
            float minx = SelectObject.SelectedObjects[0].transform.position.x;
            float miny = SelectObject.SelectedObjects[0].transform.position.y;
            float maxx = SelectObject.SelectedObjects[0].transform.position.x;
            float maxy = SelectObject.SelectedObjects[0].transform.position.y;

            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                if (minx > objectSelected.gameObject.transform.position.x)
                {
                    minx = objectSelected.gameObject.transform.position.x;
                }
                if (miny > objectSelected.gameObject.transform.position.y)
                {
                    miny = objectSelected.gameObject.transform.position.y;
                }
                if (maxx < objectSelected.gameObject.transform.position.x)
                {
                    maxx = objectSelected.gameObject.transform.position.x;
                }
                if (maxy < objectSelected.gameObject.transform.position.y)
                {
                    maxy = objectSelected.gameObject.transform.position.y;
                }
            }

            float x = (minx + maxx)/2;
            float y = (miny + maxy)/2;
            
            return (new Vector3(x, y, 0));
        }
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update ()
    {
        // Check if Q is pressed.
        if (HotkeyManager.Instance.CheckHotkey(RotateLeftHotkeyKey, KeyAction.Down))
        {
            RotateMultiObjectCounterClockWise();
        }
        // Check if E is pressed.
        else if (HotkeyManager.Instance.CheckHotkey(RotateRightHotkeyKey, KeyAction.Down))
        {
            RotateMultiObjectClockWise();
        }
    }
}
