using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class DebugDisplay : MonoBehaviour 
{
	
    //Debug log text Component.
    public Text Output;

    //Function to write argument message into DebugLog and scroll down for the fresh line.
    public void Write(string arg0)
    {
        Output.text += arg0 + "\n";
        this.gameObject.GetComponent<ScrollRect>().velocity = new Vector2(0f,1000f);
    } 
}