using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUILamp : Component2
{
    public leader[] dllconnectors = new leader[2];
    public Lamp myComponent = GUICircuit.sim.Create<Lamp>();

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        dllconnectors = new leader[2];
        myComponent = GUICircuit.sim.Create<Lamp>();
        dllconnectors[0] = myComponent.leadIn;
        dllconnectors[1] = myComponent.leadOut;

        connectors = GetComponentsInChildren<Connector>();

        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(dllconnectors[0]);
        connectors[1].assignDllconnector(dllconnectors[1]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
