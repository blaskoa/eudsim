using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    // Update is called once per frame
    void Update () {
        if (SelectObject.SelectedObject != null && this.gameObject == SelectObject.SelectedObject)
        {
            if (Input.GetKey(KeyCode.Delete))
            {
                //list connected connectors with plusconnector
                List<GameObject> connected1 = this.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected;

                //list connected connectors with minusconnector
                List<GameObject> connected2 = this.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected; 

                //first update list of connected connectors in connected component with this component
                foreach (GameObject c in connected1)
                {
                    c.gameObject.GetComponent<Connectable>().Connected.Remove(this.gameObject.transform.GetChild(0).gameObject);
                }

                foreach (GameObject c in connected2)
                {
                    c.gameObject.GetComponent<Connectable>().Connected.Remove(this.gameObject.transform.GetChild(1).gameObject);
                }

                // for each lines in scene
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Line");

                foreach (GameObject t in objs)
                {
                    //except parental line
                    if (t.transform.name != "Line")
                    {
                        //for every line connected to this component
                        if (this.gameObject.transform.GetChild(0).gameObject == t.GetComponent<Line>().Begin 
                            || this.gameObject.transform.GetChild(1).gameObject == t.GetComponent<Line>().Begin
                            || this.gameObject.transform.GetChild(0).gameObject == t.GetComponent<Line>().End
                            || this.gameObject.transform.GetChild(1).gameObject == t.GetComponent<Line>().End)
                        {                            
                            Destroy(t.gameObject);
                        }                       
                    }
                }
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
