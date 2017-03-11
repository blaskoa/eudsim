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
                    (GameObject) Instantiate(objectSelected, objectSelected.transform.position, Quaternion.identity);
                copy.GetComponent<GUICircuitComponent>()
                    .CopyValues(objectSelected.GetComponent<GUICircuitComponent>());
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
        }

        // Check for collisions - duplicated are placed on the same position as their originals so there MUST BE a collision
        GetComponent<Draggable>().Colision();
    }
}