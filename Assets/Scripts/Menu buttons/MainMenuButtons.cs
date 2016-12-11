using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

    // Flags for every pop-up panel (it's canvas)
    private bool _NewProjectCanvasOpen = false;
    private bool _SettingsCanvasOpen = false;
    private bool _AboutCanvasOpen = false;
    private bool _ReleaseNotesCanvasOpen = false;
    private bool _ContactCanvasOpen = false;
    private bool _ReportBugCanvasOpen = false;
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
        _ToolboxOpen = guiComponent.activeInHierarchy;
        GameObject checkbox = GameObject.Find("ToolboxToggle");
        GameObject button = GameObject.Find("ToolboxButton");
        
        if (_ToolboxOpen == false)
        {
            _ToolboxOpen = true;
            guiComponent.SetActive(true);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            _ToolboxOpen = false;
            guiComponent.SetActive(false);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }
    
    // Show or hide properties panel and mark menu button
	public void ShowProperties(GameObject guiComponent)
    {        
        _PropertiesOpen = guiComponent.activeInHierarchy;
        GameObject checkbox = GameObject.Find("PropertiesToggle");
        GameObject button = GameObject.Find("PropertiesButton");
        
        if (_PropertiesOpen == false)
        {
            _PropertiesOpen = true;
            guiComponent.SetActive(true);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            _PropertiesOpen = false;
            guiComponent.SetActive(false);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }
    
    // Show or hide object explorer panel and mark menu button
	public void ShowObjectExplorer(GameObject guiComponent)
    {        
        _ObjectExplorerOpen = guiComponent.activeInHierarchy;
        GameObject checkbox = GameObject.Find("ObjectExplorerToggle");
        GameObject button = GameObject.Find("ObjectExplorerButton");
        
        if (_ObjectExplorerOpen == false)
        {
            _ObjectExplorerOpen = true;
            guiComponent.SetActive(true);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            _ObjectExplorerOpen = false;
            guiComponent.SetActive(false);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
        }
    }
    
    // Show or hide debug log panel and mark menu button
	public void ShowDebug(GameObject guiComponent)
    {        
        _DebugOpen = guiComponent.activeInHierarchy;
        GameObject checkbox = GameObject.Find("DebugToggle");
        GameObject button = GameObject.Find("DebugButton");
        
        if (_DebugOpen == false)
        {
            _DebugOpen = true;
            guiComponent.SetActive(true);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.grey;
        } else
        {
            _DebugOpen = false;
            guiComponent.SetActive(false);
            checkbox.GetComponent<UnityEngine.UI.Toggle>().isOn = false;
            button.GetComponent<UnityEngine.UI.Image>().color = Color.black;
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
    
    // Show or hide File panel menu in Main menu
	public void ShowFilePanelMenu(GameObject guiComponent)
    {        
        _FilePanelMenuOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_FilePanelMenuOpen == false)
        {
            _FilePanelMenuOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _FilePanelMenuOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide Edit panel menu in Main menu
	public void ShowEditPanelMenu(GameObject guiComponent)
    {        
        _EditPanelMenuOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_EditPanelMenuOpen == false)
        {
            _EditPanelMenuOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _EditPanelMenuOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }        
    }
    
    // Show or hide View panel menu in Main menu
	public void ShowViewPanelMenu(GameObject guiComponent)
    {        
        _ViewPanelMenuOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_ViewPanelMenuOpen == false)
        {
            _ViewPanelMenuOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _ViewPanelMenuOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide Help panel menu in Main menu
	public void ShowHelpPanelMenu(GameObject guiComponent)
    {        
        _HelpPanelMenuOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_HelpPanelMenuOpen == false)
        {
            _HelpPanelMenuOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _HelpPanelMenuOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide New Project pop-up
	public void ShowNewProjectCanvas(GameObject guiComponent)
    {        
        _NewProjectCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_NewProjectCanvasOpen == false)
        {
            _NewProjectCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _NewProjectCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide Settings pop-up
	public void ShowSettingsCanvas(GameObject guiComponent)
    {        
        _SettingsCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_SettingsCanvasOpen == false)
        {
            _SettingsCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _SettingsCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide About project pop-up
	public void ShowAboutProjectCanvas(GameObject guiComponent)
    {        
        _AboutCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_AboutCanvasOpen == false)
        {
            _AboutCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _AboutCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
        // Scroll up to first row
        GameObject aboutViewport = GameObject.Find("AboutViewport");
        aboutViewport.GetComponent<UnityEngine.UI.ScrollRect>().velocity = new Vector2(-0f,-10000f);
    }
    
    // Show or hide Release notes pop-up
	public void ShowReleaseNotesCanvas(GameObject guiComponent)
    {        
        _ReleaseNotesCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_ReleaseNotesCanvasOpen == false)
        {
            _ReleaseNotesCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _ReleaseNotesCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide Contact info pop-up
	public void ShowContactInfoCanvas(GameObject guiComponent)
    {        
        _ContactCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_ContactCanvasOpen == false)
        {
            _ContactCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _ContactCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
    
    // Show or hide Bug report pop-up
	public void ShowReportBugCanvas(GameObject guiComponent)
    {        
        _ReportBugCanvasOpen = guiComponent.GetComponent<Canvas>().enabled;
        
        if (_ReportBugCanvasOpen == false)
        {
            _ReportBugCanvasOpen = true;
            guiComponent.GetComponent<Canvas>().enabled = true;
        } else
        {
            _ReportBugCanvasOpen = false;
            guiComponent.GetComponent<Canvas>().enabled = false;
        }
    }
}
