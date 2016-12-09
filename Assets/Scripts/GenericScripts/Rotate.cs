using UnityEngine;
using Assets.Scripts.Hotkeys;

public class Rotate : MonoBehaviour
{
    public const string RotateLeftHotkeyKey = "RotateLeft";
    public const string RotateRightHotkeyKey = "RotateRight";
    // Update is called once per frame
    void Update () {
        // Check if any object is selected.
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            // Check if Q is pressed.
            if (HotkeyManager.Instance.CheckHotkey(RotateLeftHotkeyKey))
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100));
            }
            // Check if E is pressed.
            else if (HotkeyManager.Instance.CheckHotkey(RotateRightHotkeyKey))
            {
                transform.Rotate(new Vector3(0, 0,  - Time.deltaTime * 100));
            }
        }
    }
}
