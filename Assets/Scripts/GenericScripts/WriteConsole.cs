using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class WriteConsole : MonoBehaviour {
 
    public Text output;
	public string message;
	
    void Start () {
        //output = gameObject.GetComponent<Text>();
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
		SubmitMessage(message);
    }
 
    private void SubmitMessage(string arg0)
    {
        //string currentText = input.text; //maybe add ToString()?
        //string newText = currentText + "\n" + arg0;
        output.text += arg0 + "\n";
    }     
 
}