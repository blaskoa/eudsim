using UnityEngine;
using Assets.Scripts.Hotkeys;

public class Rotate : MonoBehaviour
{
    public const string RotateLeftHotkeyKey = "RotateLeft";
    public const string RotateRightHotkeyKey = "RotateRight";
    //Rotate functionality to invoke rotation from button, +90 degrees
    public void RoateClockWise()
    {
        if (SelectObject.SelectedObject != null && SelectObject.SelectedObject.tag.Equals("ActiveItem"))
        {
            SelectObject.SelectedObject.transform.Rotate(new Vector3(0, 0, -90));
        }
    }

    //Rotate functionality to invoke rotation from button, -90 degrees
    public void RoateCounterClockWise()
    {
        if (SelectObject.SelectedObject != null && SelectObject.SelectedObject.tag.Equals("ActiveItem"))
        {
            SelectObject.SelectedObject.transform.Rotate(new Vector3(0, 0, +90));
        }
    }

    // Update is called once per frame
    void Update () {
        // Check if any object is selected.
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            // Check if Q is pressed.
            if (HotkeyManager.Instance.CheckHotkey(RotateLeftHotkeyKey))
            {
                RoateClockWise();
            }
            // Check if E is pressed.
            else if (HotkeyManager.Instance.CheckHotkey(RotateRightHotkeyKey))
            {
                RoateCounterClockWise();
            }
        }
    }
}
