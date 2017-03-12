using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Duplicate : MonoBehaviour
{
    public void DuplicateComponent()
    {
        List<GameObject> clones = new List<GameObject>();

        // Get all game objects and find the top-left and bottom-right most components
        foreach (GameObject objectSelected in SelectObject.SelectedObjects)
        {
            if (objectSelected.tag.Equals("ActiveItem") || objectSelected.tag.Equals("ActiveNode"))
            {
                // Instantiate new copy GameObject
                GameObject copy =
                    (GameObject) Instantiate(objectSelected, objectSelected.transform.position, objectSelected.transform.rotation);
                copy.GetComponent<GUICircuitComponent>()
                    .CopyValues(objectSelected.GetComponent<GUICircuitComponent>());

                // Clear connections of component's Plus and Minus connectors
                Connectable[] connectableScripts = copy.GetComponentsInChildren<Connectable>();
                foreach (Connectable connectableScript in connectableScripts)
                {
                    connectableScript.Connected.Clear();
                }

                // Add newly created clone to the list of clones
                clones.Add(copy);
            }
        }

        // Duplicate each line and interconnect clone objects
        foreach (GameObject line in SelectObject.SelectedLines)
        {
            GameObject duplicateLine = Instantiate(line);

            // Set the Begin of the duplicated line
            GameObject originalBegin = line.GetComponent<Line>().Begin.transform.parent.gameObject;
            string originalBeginName = line.GetComponent<Line>().Begin.name;
            int originalBeginIndex = SelectObject.SelectedObjects.IndexOf(originalBegin);
            GameObject cloneBegin = clones[originalBeginIndex];
            if (originalBeginName == "PlusConnector")
            {
                duplicateLine.GetComponent<Line>().Begin = cloneBegin.transform.FindChild("PlusConnector").gameObject;
            }
            else
            {
                duplicateLine.GetComponent<Line>().Begin = cloneBegin.transform.FindChild("MinusConnector").gameObject;
            }

            // Set the End of the duplicated line
            GameObject originalEnd = line.GetComponent<Line>().End.transform.parent.gameObject;
            string originalEndName = line.GetComponent<Line>().End.name;
            int originalEndIndex = SelectObject.SelectedObjects.IndexOf(originalEnd);
            GameObject cloneEnd = clones[originalEndIndex];
            if (originalEndName == "PlusConnector")
            {
                duplicateLine.GetComponent<Line>().End = cloneEnd.transform.FindChild("PlusConnector").gameObject;
            }
            else
            {
                duplicateLine.GetComponent<Line>().End = cloneEnd.transform.FindChild("MinusConnector").gameObject;
            }

            // Add references of each other to both newly connected connectors of the duplicated objects
            duplicateLine.GetComponent<Line>().Begin.GetComponent<Connectable>().AddConnected(duplicateLine.GetComponent<Line>().End);
            duplicateLine.GetComponent<Line>().End.GetComponent<Connectable>().AddConnected(duplicateLine.GetComponent<Line>().Begin);

            duplicateLine.GetComponent<Line>().Begin.GetComponent<Connector>().ConnectedConnectors.Add(duplicateLine.GetComponent<Line>().End.GetComponent<Connector>());
            duplicateLine.GetComponent<Line>().End.GetComponent<Connector>().ConnectedConnectors.Add(duplicateLine.GetComponent<Line>().Begin.GetComponent<Connector>());
        }

        // Check for collisions - duplicated are placed on the same position as their originals so there MUST BE a collision
        GetComponent<Draggable>().Colision();
    }
}