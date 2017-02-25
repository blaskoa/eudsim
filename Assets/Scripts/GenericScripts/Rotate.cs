using UnityEngine;
using Assets.Scripts.Hotkeys;

public class Rotate : MonoBehaviour
{
    public const string RotateLeftHotkeyKey = "RotateLeft";
    public const string RotateRightHotkeyKey = "RotateRight";
    //Rotate functionality to invoke rotation from button, +90 degrees
    public void RoateClockWise()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, +90));      
    }

    //Rotate functionality to invoke rotation from button, -90 degrees
    public void RoateCounterClockWise()
    {
         this.gameObject.transform.Rotate(new Vector3(0, 0, -90));
    }

    // Update is called once per frame
    void Update () {

        // Check if any object is selected.
        if (this.gameObject.tag.Equals("ActiveItem") && SelectObject.SelectedObjects.Contains(this.gameObject))
        {
            if (SelectObject.SelectedObjects.Count == 1)
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
}
