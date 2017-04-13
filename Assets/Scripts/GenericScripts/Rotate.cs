using UnityEngine;
using Assets.Scripts.Hotkeys;
using System.Collections;
using System.Collections.Generic;

public class Rotate : MonoBehaviour
{
    public const string RotateLeftHotkeyKey = "RotateLeft";
    public const string RotateRightHotkeyKey = "RotateRight";

    //Rotate functionality to invoke rotation from button, +90 degrees
    public void RoateClockWise()
    {
        if (SelectObject.SelectedObjects.Count == 1)
        {
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                Vector3 curentPos = objectSelected.transform.position;

                objectSelected.transform.Rotate(new Vector3(0, 0, -90));

                Vector3 finalPos = objectSelected.transform.position;

                List<float> properties = new List<float>();
                properties.Add(objectSelected.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(curentPos[0] - finalPos[0]);
                properties.Add(curentPos[1] - finalPos[1]);
                properties.Add(+90);

                PosChange change = DoUndo.dummyObj.AddComponent<PosChange>();
                change.SetChange(properties);

                UndoAction undoAction = new UndoAction();
                undoAction.AddChange(change);

                GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            }
        }

        //transform position of each lines in scene
        GameObject line = GameObject.Find("Line(Clone)");
        if (line != null)
        {
            line.GetComponent<Line>().TransformLines();
        }
    }

    //Rotate functionality to invoke rotation from button, -90 degrees
    public void RoateCounterClockWise()
    {
        if (SelectObject.SelectedObjects.Count == 1)
        {
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                Vector3 curentPos = objectSelected.transform.position;

                objectSelected.transform.Rotate(new Vector3(0, 0, +90));

                Vector3 finalPos = objectSelected.transform.position;

                List<float> properties = new List<float>();
                properties.Add(objectSelected.GetComponent<GUICircuitComponent>().GetId());
                properties.Add(curentPos[0] - finalPos[0]);
                properties.Add(curentPos[1] - finalPos[1]);
                properties.Add(-90);

                PosChange change = DoUndo.dummyObj.AddComponent<PosChange>();
                change.SetChange(properties);

                UndoAction undoAction = new UndoAction();
                undoAction.AddChange(change);

                GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            }

            //transform position of each lines in scene
            GameObject line = GameObject.Find("Line(Clone)");
            if (line != null)
            {
                line.GetComponent<Line>().TransformLines();
            }
        }
    }

    // Update is called once per frame
    void Update () {

        // Check if any object is selected.
        if (SelectObject.SelectedObjects.Contains(this.gameObject) && SelectObject.SelectedObjects.Count == 1)
        {
            // Check if Q is pressed.
            if (HotkeyManager.Instance.CheckHotkey(RotateLeftHotkeyKey, KeyAction.Down))
            {
                RoateCounterClockWise();
            }
            // Check if E is pressed.
            else if (HotkeyManager.Instance.CheckHotkey(RotateRightHotkeyKey, KeyAction.Down))
            {
                RoateClockWise();
            }        
        }
    }
}
