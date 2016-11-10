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

        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(DLLConnectors[0]);
        connectors[1].assignDllconnector(DLLConnectors[1]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
