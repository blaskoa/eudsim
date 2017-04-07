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
            UndoAction undoAction = new UndoAction();
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                GUICircuitComponent component = objectSelected.GetComponent<GUICircuitComponent>();
                List<float> prop = new List<float>();
                prop.Add((float)0.0);
                prop.Add((float)component.GetId());
                prop.Add((float)objectSelected.gameObject.transform.GetChild(0).GetComponent<Connectable>().GetID());
                prop.Add((float)objectSelected.gameObject.transform.GetChild(1).GetComponent<Connectable>().GetID());

                CreateDeleteCompChange change = DoUndo.dummyObj.AddComponent<CreateDeleteCompChange>();
                change.SetPosition(objectSelected.transform.position);
                change.SetChange(prop);
                change.SetType(objectSelected.gameObject.GetComponent<GUICircuitComponent>().GetType());
                change.RememberConnectorsToFirst(objectSelected.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected);
                change.RememberConnectorsToSecond(objectSelected.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected);
                
                undoAction.AddChange(change);

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
            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            SelectObject.SelectedLines.Clear();
            SelectObject.SelectedObjects.Clear();
        }

        
        // Destroy selected line when delete key was pressed 
        if (SelectObject.SelectedLines.Count != 0 && SelectObject.SelectedLines.Contains(this.gameObject))
        {
            UndoAction undoAction = new UndoAction();

            List<float> prop = new List<float>();
            prop.Add((float)0.0);
            prop.Add(this.gameObject.GetComponent<Line>().Begin.GetComponent<Connectable>().GetID());
            prop.Add(this.gameObject.GetComponent<Line>().End.GetComponent<Connectable>().GetID());

            CreateDeleteLineChange change = DoUndo.dummyObj.AddComponent<CreateDeleteLineChange>();
            change.SetChange(prop);
            undoAction.AddChange(change);
            GUICircuitComponent.globalUndoList.AddUndo(undoAction);

            // Delete connected connectors from lists of connectors
            this.gameObject.GetComponent<Line>().Begin.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().End.gameObject);
            this.gameObject.GetComponent<Line>().End.GetComponent<Connectable>().Connected.Remove(this.gameObject.GetComponent<Line>().Begin.gameObject);
            Destroy(this.gameObject);
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
