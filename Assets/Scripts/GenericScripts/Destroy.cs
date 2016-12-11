using System.Collections.Generic;
using UnityEngine;


public class Destroy : MonoBehaviour {

    public void DeleteSelected()
    {
        if (SelectObject.SelectedObject != null)
        {
            //list connected connectors with plusconnector
            List<GameObject> connected1 =
                SelectObject.SelectedObject.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected;

            //list connected connectors with minusconnector
            List<GameObject> connected2 =
                SelectObject.SelectedObject.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected;

            //first update list of connected connectors in connected component with this component
            foreach (GameObject c in connected1)
            {
                c.gameObject.GetComponent<Connectable>()
                    .Connected.Remove(SelectObject.SelectedObject.gameObject.transform.GetChild(0).gameObject);
            }

            foreach (GameObject c in connected2)
            {
                c.gameObject.GetComponent<Connectable>()
                    .Connected.Remove(SelectObject.SelectedObject.gameObject.transform.GetChild(1).gameObject);
            }

            // for each lines in scene
            GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");

            foreach (GameObject currentLine in lines)
            {
                //except parental line
                if (currentLine.transform.name != "Line")
                {
                    //for every line connected to this component
                    if (SelectObject.SelectedObject.gameObject.transform.GetChild(0).gameObject ==
                        currentLine.GetComponent<Line>().Begin
                        ||
                        SelectObject.SelectedObject.gameObject.transform.GetChild(1).gameObject ==
                        currentLine.GetComponent<Line>().Begin
                        ||
                        SelectObject.SelectedObject.gameObject.transform.GetChild(0).gameObject ==
                        currentLine.GetComponent<Line>().End
                        ||
                        SelectObject.SelectedObject.gameObject.transform.GetChild(1).gameObject ==
                        currentLine.GetComponent<Line>().End)
                    {
                        Destroy(currentLine.gameObject);
                    }
                }
            }
            Destroy(SelectObject.SelectedObject.gameObject);

        }

        //destroy selected line when delete key was pressed 
        if (SelectObject.SelectedObject != null &&
            (Line.SelectedLine != null && SelectObject.SelectedObject.gameObject == Line.SelectedLine))
        {
            //delete connected connectors from lists of connectors
            SelectObject.SelectedObject.gameObject.GetComponent<Line>()
                .Begin.GetComponent<Connectable>()
                .Connected.Remove(SelectObject.SelectedObject.gameObject.GetComponent<Line>().End.gameObject);
            SelectObject.SelectedObject.gameObject.GetComponent<Line>()
                .End.GetComponent<Connectable>()
                .Connected.Remove(SelectObject.SelectedObject.gameObject.GetComponent<Line>().Begin.gameObject);
            Destroy(SelectObject.SelectedObject.gameObject);
        }
       
    }

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
