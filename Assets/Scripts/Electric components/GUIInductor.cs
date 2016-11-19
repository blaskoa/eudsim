using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;
using System;
using System.Collections.Generic;

public class GUIInductor : Component2
{
    public leader[] DLLCconnectors;
    public InductorElm MyComponent;

    public double Inductance
    {
        get { return MyComponent.inductance; }
        set { MyComponent.inductance = value; }   // GUI check - accept only positive integer
    }

    public bool IsTrapezoidal
    {
        get { return MyComponent.isTrapezoidal; }
        set { MyComponent.isTrapezoidal = value; }   // GUI check - accept only true/ false
    }

    public override void getProperties()
    {
        EditObjectProperties.Add("InductancePropertyLabel", Inductance.ToString());
        EditObjectProperties.Add("TrapezoidalPropertyLabel", IsTrapezoidal.ToString());
    }

    public override void setProperties()
    {
        List<string> values = EditObjectProperties.Get();

        Inductance = double.Parse(values[0]);
        IsTrapezoidal = bool.Parse(values[1]);
    }

    // Use this for initialization
    void Start()
    {
        DLLCconnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<InductorElm>();
        DLLCconnectors[0] = MyComponent.leadIn;
        DLLCconnectors[1] = MyComponent.leadOut;

        connectors = GetComponentsInChildren<Connector>();

        connectors[0].setConnectedConnectors();
        connectors[1].setConnectedConnectors();
        connectors[0].assignComponent(this);
        connectors[1].assignComponent(this);
        connectors[0].setDllconnector(DLLCconnectors[0]);
        connectors[1].setDllconnector(DLLCconnectors[1]);
    }
}
