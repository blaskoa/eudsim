using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	private bool _NewProjectCanvasOpen = false;
	private bool _FilePanelMenuOpen = false;
	private bool _EditPanelMenuOpen = false;
	private bool _ViewPanelMenuOpen = false;
	private bool _HelpPanelMenuOpen = false;
	
	public void FilePanelMenu(GameObject obj)
    {		
	    _FilePanelMenuOpen = obj.active;
		
	    if (_FilePanelMenuOpen == false)
	    {
	        _FilePanelMenuOpen = true;
	        obj.SetActive(true);
	    } else
	    {
	        _FilePanelMenuOpen = false;
	        obj.SetActive(false);
	    }
    }
	
	public void EditPanelMenu(GameObject obj)
    {		
	    _EditPanelMenuOpen = obj.active;
		
	    if (_EditPanelMenuOpen == false)
	    {
	        _EditPanelMenuOpen = true;
	        obj.SetActive(true);
	    } else
	    {
	        _EditPanelMenuOpen = false;
	        obj.SetActive(false);
	    }
    }
	
	public void ViewPanelMenu(GameObject obj)
    {		
	    _ViewPanelMenuOpen = obj.active;
		
	    if (_ViewPanelMenuOpen == false)
	    {
	        _ViewPanelMenuOpen = true;
	        obj.SetActive(true);
	    } else
	    {
	        _ViewPanelMenuOpen = false;
	        obj.SetActive(false);
	    }
    }
	
	public void HelpPanelMenu(GameObject obj)
    {		
	    _HelpPanelMenuOpen = obj.active;
		
	    if (_HelpPanelMenuOpen == false)
	    {
	        _HelpPanelMenuOpen = true;
	        obj.SetActive(true);
	    } else
	    {
	        _HelpPanelMenuOpen = false;
	        obj.SetActive(false);
	    }
    }
	
    public void NewProjectCanvas(GameObject obj)
    {		
	    _NewProjectCanvasOpen = obj.active;
		
	    if (_NewProjectCanvasOpen == false)
	    {
	        _NewProjectCanvasOpen = true;
	        obj.SetActive(true);
	    } else
	    {
	        _NewProjectCanvasOpen = false;
	        obj.SetActive(false);
	    }
    }
}
