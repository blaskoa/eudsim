using UnityEngine;

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

        //destroy selected line when delete key was pressed 
        if (Line.SelectedLine != null && this.gameObject == Line.SelectedLine)
        {
            if (Input.GetKey(KeyCode.Delete))
            {
                //delete connected connectors from lists of connectors
                this.gameObject.GetComponent<Line>().Begin.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().End.gameObject);
                this.gameObject.GetComponent<Line>().End.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().Begin.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
