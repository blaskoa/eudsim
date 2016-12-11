using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    //Rotate functionality to invoke rotation from button, +90 degrees
    public void RoateClockWise()
    {
        if (SelectObject.SelectedObject != null)
        {
            SelectObject.SelectedObject.transform.Rotate(new Vector3(0, 0, +90));
        }
    }

    //Rotate functionality to invoke rotation from button, -90 degrees
    public void RoateCounterClockWise()
    {
        if (SelectObject.SelectedObject != null)
        {
            SelectObject.SelectedObject.transform.Rotate(new Vector3(0, 0, -90));
        }
    }

    // Update is called once per frame
    void Update () {
        // Check if any object is selected.
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            // Check if Q is pressed.
            if (Input.GetKey("q"))
            {
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * 100));
            }
            // Check if E is pressed.
            else if (Input.GetKey("e"))
            {
                transform.Rotate(new Vector3(0, 0,  - Time.deltaTime * 100));
            }
        }
    }
}
