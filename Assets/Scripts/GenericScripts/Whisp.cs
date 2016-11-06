using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Whisp : MonoBehaviour 
{
 
	private DebugDisplay debugLog;
	private GameObject dl;
	/*
	Object could call this from their functions by:
	this.GetComponent<Whisp>().Say("Hello from the other side.");
	*/
	void Start()
    {
		dl = GameObject.Find("DebugLog");
		debugLog = (DebugDisplay) dl.GetComponent(typeof(DebugDisplay));
		Say("Hello, I am " + this.gameObject.name + "! :)");
    }
	
	//Send message into debug log.
    public void Say (string arg0) 
	{
		debugLog.Write(arg0);
    }   
 
}