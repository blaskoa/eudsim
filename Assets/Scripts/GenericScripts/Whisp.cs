using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Whisp : MonoBehaviour 
{
 
    private DebugDisplay _debugLog;
    private GameObject _dl;
	
    /*
    Object could call this from their functions by:
    this.GetComponent<Whisp>().Say("Hello from the other side.");
    */
    void Start()
    {
        _dl = GameObject.Find("DebugLog");
        _debugLog = (DebugDisplay) _dl.GetComponent(typeof(DebugDisplay));
        Say("Hello, I am " + this.gameObject.name + "! :)");
    }

    //Send message into debug log.
    public void Say (string arg0) 
    {
        _debugLog.Write(arg0);
    } 
}