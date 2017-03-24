using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
   
    void Start()
    {
        //Initialization, program is paused
        PlayPauseButton("pause");
        GameObject toolbox = GameObject.Find("ToolboxButton");
        GameObject properties = GameObject.Find("PropertiesButton");
        GameObject objectExplorer = GameObject.Find("ObjectExplorerButton");
        GameObject debug = GameObject.Find("DebugButton");
        toolbox.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        properties.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        objectExplorer.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        debug.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        
        // Scroll up to first row
        GameObject aboutViewport = GameObject.Find("AboutViewport");
        aboutViewport.GetComponent<UnityEngine.UI.ScrollRect>().velocity = new Vector2(-0f,-10000f);
    }
    
    // Function to handle play and pause buttons
    public void PlayPauseButton(string action)
    {    
        // Find all components for play/pause
        GameObject playButton = GameObject.Find("PlayButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        GameObject checkboxPlay = GameObject.Find("PlayToggle");
        GameObject checkboxPause = GameObject.Find("PauseToggle");
        GameObject menuPlayButton = GameObject.Find("MenuPlayButton");
        GameObject menuPauseButton = GameObject.Find("MenuPauseButton");
        
        // Set GUI components for play/pause
        if (action == "play")
        {
            checkboxPlay.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            checkboxPause.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            menuPlayButton.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
            menuPauseButton.GetComponent<UnityEngine.UI.Image>().color = Color.black;
            playButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
            pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        } else
        {
            checkboxPlay.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            checkboxPause.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            menuPlayButton.GetComponent<UnityEngine.UI.Image>().color = Color.black;
            menuPauseButton.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
            playButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
            pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }
    
    // Show or hide toolbox panel and mark menu button
	public void ShowToolbox(GameObject guiComponent)
    {        
        GameObject checkbox = GameObject.Find("ToolboxToggle");
        GameObject button = GameObject.Find("ToolboxButton");
        
        if (guiComponent.activeSelf == false)
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        
        ShowPanel(guiComponent);
    }
    
    // Show or hide properties panel and mark menu button
	public void ShowProperties(GameObject guiComponent)
    {        
        GameObject checkbox = GameObject.Find("PropertiesToggle");
        GameObject button = GameObject.Find("PropertiesButton");
        
        if (guiComponent.activeSelf == false)
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        
        ShowPanel(guiComponent);
    }
    
    // Show or hide object explorer panel and mark menu button
	public void ShowObjectExplorer(GameObject guiComponent)
    {        
        GameObject checkbox = GameObject.Find("ObjectExplorerToggle");
        GameObject button = GameObject.Find("ObjectExplorerButton");
        
        if (guiComponent.activeSelf == false)
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        
        ShowPanel(guiComponent);
    }
    
    // Show or hide debug log panel and mark menu button
	public void ShowDebug(GameObject guiComponent)
    {        
        GameObject checkbox = GameObject.Find("DebugToggle");
        GameObject button = GameObject.Find("DebugButton");
        
        if (guiComponent.activeSelf == false)
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
        
        ShowPanel(guiComponent);
    }
    
    // Hide or show panel
    public void ShowPanel(GameObject guiComponent)
    {      
        if (guiComponent.activeSelf == false)
        {
            guiComponent.SetActive(true);
        } else
        {
            guiComponent.SetActive(false);
        }
    }
    
    // Open basic explorer after buttonClick to Open project
	public void OpenProject()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select,");
    }
    
    // Open basic explorer after buttonClick to Save project
	public void SaveProject()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select,");
    }
    
    // Open basic explorer after buttonClick to Save As project
	public void SaveAsProject()
    {
        System.Diagnostics.Process.Start("explorer.exe", "/select,");
    }
    
    // Show or hide given canvas
	public void ShowCanvas(GameObject guiComponent)
    {
        if (guiComponent.GetComponent<Canvas>().enabled == false)
        {
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
}
