using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (SelectObject.selectedObject != null && this.gameObject == SelectObject.selectedObject)
        {
            if (Input.GetKey(KeyCode.Delete))
            {
                Destroy(this.gameObject);
            }
        }
	}
}
