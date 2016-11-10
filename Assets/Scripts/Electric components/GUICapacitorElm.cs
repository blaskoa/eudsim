using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUICapacitorElm : Component2
{
    public leader[] dllconnectors;
    CapacitorElm myComponent;

    // Use this for initialization
    void Start()
    {
        dllconnectors = new leader[2];
        myComponent = GUICircuit.sim.Create<CapacitorElm>();
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
