using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeysUtils
{

    public static void DisableAllMonoScripts()
    {
       //treba to spravit tak, ze najdem  vsetky scripty so specifickym nazvom a disablnem ho v kazdom objekte
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            Destroy[] destroys = go.GetComponentsInChildren<Destroy>();
            foreach (Destroy scripts in destroys)
            {
                scripts.enabled = false;
            }

            RotateMultiObject[] rotateMultiObjects = go.GetComponentsInChildren<RotateMultiObject>();
            foreach (RotateMultiObject scripts in rotateMultiObjects)
            {
                scripts.enabled = false;
            }

            Rotate[] rotate = go.GetComponentsInChildren<Rotate>();
            foreach (Rotate scripts in rotate)
            {
                scripts.enabled = false;
            }

            DragWithKeys[] dragWithKeyses = go.GetComponentsInChildren<DragWithKeys>();
            foreach (DragWithKeys scripts in dragWithKeyses)
            {
                scripts.enabled = false;
            }

            SwitchTypeLine[] switchTypeLines = go.GetComponentsInChildren<SwitchTypeLine>();
            foreach (SwitchTypeLine scripts in switchTypeLines)
            {
                scripts.enabled = false;
            }
        }
    }

    public static void EnableAllMonoScripts()
    {
        //treba to spravit tak, ze najdem  vsetky scripty so specifickym nazvom a disablnem ho v kazdom objekte
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            Destroy[] destroys = go.GetComponentsInChildren<Destroy>();
            foreach (Destroy scripts in destroys)
            {
                scripts.enabled = true;
            }

            RotateMultiObject[] rotateMultiObjects = go.GetComponentsInChildren<RotateMultiObject>();
            foreach (RotateMultiObject scripts in rotateMultiObjects)
            {
                scripts.enabled = true;
            }

            Rotate[] rotate = go.GetComponentsInChildren<Rotate>();
            foreach (Rotate scripts in rotate)
            {
                scripts.enabled = true;
            }

            DragWithKeys[] dragWithKeyses = go.GetComponentsInChildren<DragWithKeys>();
            foreach (DragWithKeys scripts in dragWithKeyses)
            {
                scripts.enabled = true;
            }

            SwitchTypeLine[] switchTypeLines = go.GetComponentsInChildren<SwitchTypeLine>();
            foreach (SwitchTypeLine scripts in switchTypeLines)
            {
                scripts.enabled = true;
            }
        }
    }
}
