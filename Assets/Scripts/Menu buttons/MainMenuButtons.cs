using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	private bool _NewProjectCanvasOpen = false;
	private bool _FilePanelMenuOpen = false;
	private bool _EditPanelMenuOpen = false;
	private bool _ViewPanelMenuOpen = false;
	private bool _HelpPanelMenuOpen = false;
	private bool _ToolboxOpen = false;
	private bool _PropertiesOpen = false;
	private bool _ObjectExplorerOpen = false;
	private bool _DebugOpen = false;
	
	void Start()
	{
		//Initialization
		PauseButton();
		GameObject toolbox = GameObject.Find("ToolboxButton");
		GameObject properties = GameObject.Find("PropertiesButton");
		GameObject objectExplorer = GameObject.Find("ObjectExplorerButton");
		GameObject debug = GameObject.Find("DebugButton");
		toolbox.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
		properties.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
		objectExplorer.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
		debug.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
	}
	
	public void PlayButton()
    {		
		GameObject playButton = GameObject.Find("PlayButton");
		GameObject pauseButton = GameObject.Find("PauseButton");
		GameObject checkboxPlay = GameObject.Find("PlayToggle");
		GameObject checkboxPause = GameObject.Find("PauseToggle");
		GameObject menuPlayButton = GameObject.Find("MenuPlayButton");
		GameObject menuPauseButton = GameObject.Find("MenuPauseButton");
		checkboxPlay.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
		checkboxPause.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
		menuPlayButton.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
		menuPauseButton.GetComponent<UnityEngine.UI.Image>().color = Color.black;
		playButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
		pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }
	
	public void PauseButton()
    {		
		GameObject playButton = GameObject.Find("PlayButton");
		GameObject pauseButton = GameObject.Find("PauseButton");
		GameObject checkboxPlay = GameObject.Find("PlayToggle");
		GameObject checkboxPause = GameObject.Find("PauseToggle");
		GameObject menuPlayButton = GameObject.Find("MenuPlayButton");
		GameObject menuPauseButton = GameObject.Find("MenuPauseButton");
		checkboxPlay.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
		checkboxPause.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
		menuPlayButton.GetComponent<UnityEngine.UI.Image>().color = Color.black;
		menuPauseButton.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
		playButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
		pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
	
    public void ShowToolbox(GameObject obj)
    {		
	    _ToolboxOpen = obj.activeInHierarchy;
		GameObject checkbox = GameObject.Find("ToolboxToggle");
		GameObject button = GameObject.Find("ToolboxButton");
		
	    if (_ToolboxOpen == false)
	    {
	        _ToolboxOpen = true;
	        obj.SetActive(true);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
	    } else
	    {
	        _ToolboxOpen = false;
	        obj.SetActive(false);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
	    }
    }
	
	public void ShowProperties(GameObject obj)
    {		
	    _PropertiesOpen = obj.activeInHierarchy;
        GameObject checkbox = GameObject.Find("PropertiesToggle");
		GameObject button = GameObject.Find("PropertiesButton");
		
	    if (_PropertiesOpen == false)
	    {
	        _PropertiesOpen = true;
	        obj.SetActive(true);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
	    } else
	    {
	        _PropertiesOpen = false;
	        obj.SetActive(false);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
	    }
    }
	
	public void ShowObjectExplorer(GameObject obj)
    {		
	    _ObjectExplorerOpen = obj.activeInHierarchy;
        GameObject checkbox = GameObject.Find("ObjectExplorerToggle");
		GameObject button = GameObject.Find("ObjectExplorerButton");
		
	    if (_ObjectExplorerOpen == false)
	    {
	        _ObjectExplorerOpen = true;
	        obj.SetActive(true);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
	    } else
	    {
	        _ObjectExplorerOpen = false;
	        obj.SetActive(false);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
	    }
    }
	
	public void ShowDebug(GameObject obj)
    {		
	    _DebugOpen = obj.activeInHierarchy;
        GameObject checkbox = GameObject.Find("DebugToggle");
		GameObject button = GameObject.Find("DebugButton");
		
	    if (_DebugOpen == false)
	    {
	        _DebugOpen = true;
	        obj.SetActive(true);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
	    } else
	    {
	        _DebugOpen = false;
	        obj.SetActive(false);
			checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
			button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
	    }
    }
	
	public void OpenProject()
	{
	    System.Diagnostics.Process.Start("explorer.exe", "/select,");
	}
	
	public void SaveProject()
	{
	    System.Diagnostics.Process.Start("explorer.exe", "/select,");
	}
	
	public void SaveAsProject()
	{
	    System.Diagnostics.Process.Start("explorer.exe", "/select,");
	}
	
	public void FilePanelMenu(GameObject obj)
    {		
	    _FilePanelMenuOpen = obj.GetComponent<Canvas>().enabled;
		
	    if (_FilePanelMenuOpen == false)
	    {
	        _FilePanelMenuOpen = true;
	        obj.GetComponent<Canvas>().enabled = true;
	    } else
	    {
	        _FilePanelMenuOpen = false;
	        obj.GetComponent<Canvas>().enabled = false;
	    }
    }
	
	public void EditPanelMenu(GameObject obj)
    {		
	    _EditPanelMenuOpen = obj.GetComponent<Canvas>().enabled;
		
	    if (_EditPanelMenuOpen == false)
	    {
	        _EditPanelMenuOpen = true;
			obj.GetComponent<Canvas>().enabled = true;
	    } else
	    {
	        _EditPanelMenuOpen = false;
			obj.GetComponent<Canvas>().enabled = false;
	    }		
    }
	
	public void ViewPanelMenu(GameObject obj)
    {		
	    _ViewPanelMenuOpen = obj.GetComponent<Canvas>().enabled;
		
	    if (_ViewPanelMenuOpen == false)
	    {
	        _ViewPanelMenuOpen = true;
	        obj.GetComponent<Canvas>().enabled = true;
	    } else
	    {
	        _ViewPanelMenuOpen = false;
	        obj.GetComponent<Canvas>().enabled = false;
	    }
    }
	
	public void HelpPanelMenu(GameObject obj)
    {		
	    _HelpPanelMenuOpen = obj.GetComponent<Canvas>().enabled;
		
	    if (_HelpPanelMenuOpen == false)
	    {
	        _HelpPanelMenuOpen = true;
	        obj.GetComponent<Canvas>().enabled = true;
	    } else
	    {
	        _HelpPanelMenuOpen = false;
	        obj.GetComponent<Canvas>().enabled = false;
	    }
    }
	
    public void NewProjectCanvas(GameObject obj)
    {		
	    _NewProjectCanvasOpen = obj.GetComponent<Canvas>().enabled;
		
	    if (_NewProjectCanvasOpen == false)
	    {
	        _NewProjectCanvasOpen = true;
	        obj.GetComponent<Canvas>().enabled = true;
	    } else
	    {
	        _NewProjectCanvasOpen = false;
	        obj.GetComponent<Canvas>().enabled = false;
	    }
    }
}
