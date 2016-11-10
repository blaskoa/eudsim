using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class Whisp : MonoBehaviour 
{
 
    public DebugDisplay DebugLog;
    public GameObject Dl;
	
    /*
    Object could call this from their functions by:
    this.GetComponent<Whisp>().Say("Hello from the other side.");
    */
    void Start()
    {
        Dl = GameObject.Find("DebugLog");
        DebugLog = (DebugDisplay) Dl.GetComponent(typeof(DebugDisplay));
        Say("Hello, I am " + this.gameObject.name + "! :)");
    }

    //Send message into debug log.
    public void Say (string arg0) 
    {
        DebugLog.Write(arg0);
    } 
}