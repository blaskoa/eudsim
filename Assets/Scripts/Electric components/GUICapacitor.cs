using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUICapacitor : Component2
{
    public leader[] DllConnectors;
    public CapacitorElm MyComponent;

    // Use this for initialization
    void Start()
    {
        DllConnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<CapacitorElm>();
        DllConnectors[0] = MyComponent.leadIn;
        DllConnectors[1] = MyComponent.leadOut;
        connectors = GetComponentsInChildren<Connector>();

        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(DllConnectors[0]);
        connectors[1].assignDllconnector(DllConnectors[1]);
    }
}
