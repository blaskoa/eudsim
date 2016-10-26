using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Check if any object is selected.
        if (SelectObject.selectedObject != null && this.gameObject == SelectObject.selectedObject)
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
