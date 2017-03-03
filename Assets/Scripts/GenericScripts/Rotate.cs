using UnityEngine;
using Assets.Scripts.Hotkeys;

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
                objectSelected.transform.Rotate(new Vector3(0, 0, +90));
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
                objectSelected.transform.Rotate(new Vector3(0, 0, -90));
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
