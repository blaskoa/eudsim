using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;
using System;
using System.Collections.Generic;

public class GUIResistor : Component2
{
    public leader[] DLLConnectors;
    public Resistor MyComponent;

    public double Resistance
    {
        get { return MyComponent.resistance; }
        set { MyComponent.resistance = value; }   // GUI check - accept only positive integer
    }

    public double getVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }

    public override void getProperties()
    {
        EditObjectProperties.Add("Resistance", Resistance.ToString());
    }

    public override void setProperties()
    {
        List<string> values = EditObjectProperties.Get();

        Resistance = Double.Parse(values[0]);
    }

    // Use this for initialization
    public void Start()
    {
        DLLConnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<Resistor>();
        DLLConnectors[0] = MyComponent.leadIn;
        DLLConnectors[1] = MyComponent.leadOut;

        //connectors = GetComponentsInChildren<Connector>();
        connectors[0] = gameObject.AddComponent<Connector>();
        connectors[1] = gameObject.AddComponent<Connector>();
        connectors[0].setConnectedConnectors();
        connectors[1].setConnectedConnectors();
        connectors[0].assignComponent(this);
        connectors[1].assignComponent(this);
        connectors[0].setDllconnector(DLLConnectors[0]);
        connectors[1].setDllconnector(DLLConnectors[1]);
    }
}
