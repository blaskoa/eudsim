using System.Collections.Generic;
using Assets.Scripts.Hotkeys;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public void DeleteSelected()
    {
        if (SelectObject.SelectedObject != null && SelectObject.SelectedObject.tag.Equals("ActiveItem"))
        {
            // List connected connectors with plusconnector
            List<GameObject> connected1 =
                SelectObject.SelectedObject.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected;

            // List connected connectors with minusconnector
            List<GameObject> connected2 =
                SelectObject.SelectedObject.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected;

            // First update list of connected connectors in connected component with this component
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

            // For each lines in scene
            GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");

            foreach (GameObject currentLine in lines)
            {
                // Except parental line
                if (currentLine.transform.name != "Line")
                {
                    // For every line connected to this component
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


        // Destroy selected line when delete key was pressed 
        if (Line.SelectedLine != null && this.gameObject == Line.SelectedLine)
        {
            if (HotkeyManager.Instance.CheckHotkey(DeleteHotkeyKey, KeyAction.Down))
            {
                // Delete connected connectors from lists of connectors
                this.gameObject.GetComponent<Line>().Begin.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().End.gameObject);
                this.gameObject.GetComponent<Line>().End.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().Begin.gameObject);
                Destroy(this.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Delete))
        {
            DeleteSelected();
        }
    }
}
