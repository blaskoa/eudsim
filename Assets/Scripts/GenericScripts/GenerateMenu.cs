using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class GenerateMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject contextMenu = GameObject.Find("ContextMenuCanvas");
        contextMenu.GetComponent<Canvas>().enabled = false; // Disable first, if menu is already open
        
        // Disable all context menu buttons to be enabled later
        for (int i = 0; i < contextMenu.transform.GetChild(0).transform.childCount; i++)
        {
            contextMenu.transform.GetChild(0).GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
        
        // Only for rightclick
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            contextMenu.GetComponent<Canvas>().enabled = true;
            contextMenu.transform.position = Camera.main.ScreenToWorldPoint(eventData.position); // convert mouse to screen position
            contextMenu.transform.position = new Vector3(contextMenu.transform.position.x + 0.7f, contextMenu.transform.position.y - 0.75f, 0.0f); // Move slightly to the right down from mouse

            // Future "copy" and "paste" buttons
            if (true)
            {
                for (int i = 1; i < 3; i++)
                {
                    contextMenu.transform.GetChild(0).GetChild(i).GetComponent<UnityEngine.UI.Button>().interactable = true;
                }
            }
            
            // Future "cut" button, Destroy AND Copy
            if (this.GetComponent<Destroy>() != null && true)
            {
                contextMenu.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            
            // Delete button
            if (this.GetComponent<Destroy>() != null)
            {
                contextMenu.transform.GetChild(0).GetChild(3).GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }
    }
}
