﻿using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
//using Assets.Skripts.Circuit;
using SharpCircuit;

public class Accumulator : MonoBehaviour, ComponentInterface {
    Connector[] connectors;
    
    VoltageInput myComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    Resistor myComponent2 = GUICircuit.sim.Create<Resistor>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
