using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;

public class GUIResistor : MonoBehaviour, ComponentInterface {
    float resistance;
    float resistorTolerance;
    float resistorLoad;
    float allowedVoltage;
    float temperatureCoefficient;

    Connector[] connectors;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
