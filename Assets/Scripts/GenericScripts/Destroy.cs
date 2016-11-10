using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
    // Update is called once per frame
    void Update () {
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            if (Input.GetKey(KeyCode.Delete))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
