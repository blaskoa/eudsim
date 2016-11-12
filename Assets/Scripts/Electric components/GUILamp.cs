using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUILamp : Component2
{
    public leader[] DLLConnectors = new leader[2];
    public Lamp MyComponent = GUICircuit.sim.Create<Lamp>();

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        DLLConnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<Lamp>();
        DLLConnectors[0] = MyComponent.leadIn;
        DLLConnectors[1] = MyComponent.leadOut;

        connectors = GetComponentsInChildren<Connector>();

        connectors[0].setConnectedConnectors();
        connectors[1].setConnectedConnectors();
        connectors[0].assignComponent(this);
        connectors[1].assignComponent(this);
        connectors[0].setDllconnector(DLLConnectors[0]);
        connectors[1].setDllconnector(DLLConnectors[1]);
    }
}
