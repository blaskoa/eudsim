using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarButtonUtils
{

    public void EnableToolbarButtons()
    {
        // Enable buttons for component manipulation
        GameObject rotateLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rotateRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rotateLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        rotateRightButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void DisableToolbarButtons()
    {
        // Disable buttons for component manipulation
        GameObject rotateLeftButton = GameObject.Find("RotateLeftButton");
        GameObject rotateRightButton = GameObject.Find("RotateRightButton");
        GameObject deleteButton = GameObject.Find("DeleteButton");
        GameObject menuDeleteButton = GameObject.Find("MenuDeleteButton");
        rotateLeftButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        rotateRightButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        deleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        menuDeleteButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

}
