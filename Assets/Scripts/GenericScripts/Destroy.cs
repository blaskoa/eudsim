using System;
using System.Collections.Generic;
using Assets.Scripts.Hotkeys;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public const string DeleteHotkeyKey = "Delete";
    public void DeleteSelected()
    {
        if (SelectObject.SelectedObjects.Count != 0)
        {
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                if (objectSelected.tag.Equals("ActiveItem"))
                {
                    // List connected connectors with plusconnector
                    List<GameObject> connected1 =
                        objectSelected.transform.GetChild(0).GetComponent<Connectable>().Connected;

                    // List connected connectors with minusconnector
                    List<GameObject> connected2 =
                        objectSelected.transform.GetChild(1).GetComponent<Connectable>().Connected;

                    // First update list of connected connectors in connected component with this component
                    foreach (GameObject c in connected1)
                    {
                        c.gameObject.GetComponent<Connectable>()
                            .Connected.Remove(objectSelected.transform.GetChild(0).gameObject);
                    }

                    foreach (GameObject c in connected2)
                    {
                        c.gameObject.GetComponent<Connectable>()
                            .Connected.Remove(objectSelected.transform.GetChild(1).gameObject);
                    }

                    // For each lines in scene
                    GameObject[] lines = GameObject.FindGameObjectsWithTag("ActiveLine");

                    foreach (GameObject currentLine in lines)
                    {
                        // For every line connected to this component
                        if (objectSelected.transform.GetChild(0).gameObject ==
                            currentLine.GetComponent<Line>().Begin
                            ||
                            objectSelected.transform.GetChild(1).gameObject ==
                            currentLine.GetComponent<Line>().Begin
                            ||
                            objectSelected.transform.GetChild(0).gameObject ==
                            currentLine.GetComponent<Line>().End
                            ||
                            objectSelected.transform.GetChild(1).gameObject ==
                            currentLine.GetComponent<Line>().End)
                        {
                            Destroy(currentLine.gameObject);
                        }
                    }                
                    Destroy(objectSelected);
                }                                            
            }
            SelectObject.SelectedLines.Clear();
            SelectObject.SelectedObjects.Clear();
        }

        
        // Destroy selected line when delete key was pressed 
        if (SelectObject.SelectedLines.Count != 0 && SelectObject.SelectedLines.Contains(this.gameObject))
        {
            // Delete connected connectors from lists of connectors
            this.gameObject.GetComponent<Line>().Begin.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().End.gameObject);
            this.gameObject.GetComponent<Line>().End.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().Begin.gameObject);
            SelectObject.SelectedLines.Remove(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
      
        if (HotkeyManager.Instance.CheckHotkey(DeleteHotkeyKey, KeyAction.Down))
        {
            DeleteSelected();
        }
    }


}
