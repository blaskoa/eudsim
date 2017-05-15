using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Duplicate : MonoBehaviour
{
    public void DuplicateComponent()
    {
        List<GameObject> cloneComponents = new List<GameObject>();
        List<GameObject> cloneLines = new List<GameObject>();

        // Get all game objects and find the top-left and bottom-right most components
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {
            if (objectSelected.tag.Equals("ActiveItem") || objectSelected.tag.Equals("ActiveNode"))
            {
                // Instantiate new copy GameObject
                GameObject copy =
                    (GameObject)Instantiate(objectSelected, objectSelected.transform.position, objectSelected.transform.rotation);
                copy.GetComponent<GUICircuitComponent>()
                    .CopyValues(objectSelected.GetComponent<GUICircuitComponent>());
                copy.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;

                // Clear connections of component's Plus and Minus connectors
                Connectable[] connectableScripts = copy.GetComponentsInChildren<Connectable>();
                foreach (Connectable connectableScript in connectableScripts)
                {
                    connectableScript.Connected.Clear();
                }

                // Add newly created clone to the list of clones
                cloneComponents.Add(copy);
            }
        }

        // Duplicate each line and interconnect clone objects
        foreach (GameObject line in SelectObject.SelectedLines)
        {
            GameObject duplicateLine = Instantiate(line);
            duplicateLine.GetComponent<Line>().KeepColiders();

            // Set the Begin of the duplicated line
            GameObject beginComponent = line.GetComponent<Line>().Begin.transform.parent.gameObject;
            // Get the index of Connector among component children
            int beginChildConnectorIndex = -1;
            GameObject beginConnector = line.GetComponent<Line>().Begin;
            for (int i = 0; i < beginComponent.transform.childCount; i++)
            {
                if (beginComponent.transform.GetChild(i).gameObject.GetInstanceID() == beginConnector.GetInstanceID())
                {
                    beginChildConnectorIndex = i;
                }
            }
            // Get index of component in the List of SelectedObjects
            int beginComponentIndex = SelectObject.SelectedObjects.IndexOf(beginComponent);
            GameObject cloneBegin = cloneComponents[beginComponentIndex];
            duplicateLine.GetComponent<Line>().Begin = cloneBegin.transform.GetChild(beginChildConnectorIndex).gameObject;

            // Set the End of the duplicated line
            GameObject endComponent = line.GetComponent<Line>().End.transform.parent.gameObject;
            // Get the index of Connector among component children
            int endChildConnectorIndex = -1;
            GameObject endConnector = line.GetComponent<Line>().End;
            for (int i = 0; i < endComponent.transform.childCount; i++)
            {
                if (endComponent.transform.GetChild(i).gameObject.GetInstanceID() == endConnector.GetInstanceID())
                {
                    endChildConnectorIndex = i;
                }
            }
            // Get index of component in the List of SelectedObjects
            int endComponentIndex = SelectObject.SelectedObjects.IndexOf(endComponent);
            GameObject cloneEnd = cloneComponents[endComponentIndex];
            duplicateLine.GetComponent<Line>().End = cloneEnd.transform.GetChild(endChildConnectorIndex).gameObject;

            // Add references of each other to both newly connected connectors of the duplicated objects
            duplicateLine.GetComponent<Line>().Begin.GetComponent<Connectable>().AddConnected(duplicateLine.GetComponent<Line>().End);
            duplicateLine.GetComponent<Line>().End.GetComponent<Connectable>().AddConnected(duplicateLine.GetComponent<Line>().Begin);

            duplicateLine.GetComponent<Line>().Begin.GetComponent<Connector>().ConnectedConnectors.Add(duplicateLine.GetComponent<Line>().End.GetComponent<Connector>());
            duplicateLine.GetComponent<Line>().End.GetComponent<Connector>().ConnectedConnectors.Add(duplicateLine.GetComponent<Line>().Begin.GetComponent<Connector>());

            // Add new line to the list of duplicated lines
            cloneLines.Add(duplicateLine);
        }
        
        SelectObject selectionComponent = GameObject.Find("Canvas").GetComponent<SelectObject>();
        selectionComponent.DeselectObject();
        selectionComponent.DeselectLine();
        SelectObject.AddItemsToSelection(cloneComponents);
        SelectObject.AddLinesToSelection(cloneLines);

        // Check for collisions - duplicated are placed on the same position as their originals so there MUST BE a collision
        GetComponent<Draggable>().Colision();

        UndoAction undoAction = new UndoAction();
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {
            if (objectSelected.tag.Equals("ActiveItem") || objectSelected.tag.Equals("ActiveNode"))
            {
                GUICircuitComponent component = objectSelected.GetComponent<GUICircuitComponent>();
                List<float> prop = new List<float>();
                prop.Add((float)1.0);
                prop.Add((float)component.GetId());
                prop.Add((float)objectSelected.gameObject.transform.GetChild(0).GetComponent<Connectable>().GetID());
                prop.Add((float)objectSelected.gameObject.transform.GetChild(1).GetComponent<Connectable>().GetID());

                CreateDeleteCompChange change = new CreateDeleteCompChange();
                change.SetPosition(objectSelected.transform.position);
                change.SetChange(prop);
                change.SetType(objectSelected.gameObject.GetComponent<GUICircuitComponent>().GetType());
                change.RememberConnectorsToFirst(objectSelected.gameObject.transform.GetChild(0).GetComponent<Connectable>().Connected);
                change.RememberConnectorsToSecond(objectSelected.gameObject.transform.GetChild(1).GetComponent<Connectable>().Connected);

                undoAction.AddChange(change);
            }
        }
        GUICircuitComponent.globalUndoList.AddUndo(undoAction);
    }
}