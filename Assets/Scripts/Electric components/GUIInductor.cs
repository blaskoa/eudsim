using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIInductor : Component2
{
    public leader[] DLLCconnectors;
    public InductorElm MyComponent;
    
    // Use this for initialization
    void Start()
    {
        DLLCconnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<InductorElm>();
        DLLCconnectors[0] = MyComponent.leadIn;
        DLLCconnectors[1] = MyComponent.leadOut;

        connectors = GetComponentsInChildren<Connector>();

        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(DLLCconnectors[0]);
        connectors[1].assignDllconnector(DLLCconnectors[1]);
    }
}
