using UnityEngine;
using System.Collections;

public class Deselect : MonoBehaviour
{
    // When left mouse button is pressed...
    void OnMouseDown()
    {
        if (SelectObject.SelectedObject != null)
        {
            SelectObject.SelectedObject.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            SelectObject.SelectedObject = null;
        }

        if (Line.SelectedLine != null)
        {
            Line.SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            Line.SelectedLine = null;
        }
    }
}
