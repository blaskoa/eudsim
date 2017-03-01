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
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                objectSelected.transform.RotateAround(point, new Vector3(0, 0, 1), +90);
            }
        }
    }

    public void RotateMultiObjectCounterClockWise()
    {
        Vector3 point = CalculateCenterPoint();
        if (SelectObject.SelectedObjects.Count > 1)
        {          
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                objectSelected.transform.RotateAround(point, new Vector3(0, 0, 1), -90);
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
