using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIAnalogSwitch : Component2
{
    public leader[] dllconnectors;
    public AnalogSwitch MyComponent;

    // Use this for initialization
    void Start()
    {
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            dllconnectors = new leader[2];
            MyComponent = GUICircuit.sim.Create<AnalogSwitch>();
            dllconnectors[0] = MyComponent.leadIn;
            dllconnectors[1] = MyComponent.leadOut;

            connectors = GetComponentsInChildren<Connector>();

            connectors[0].setConnectedConnectors();
            connectors[1].setConnectedConnectors();
            connectors[0].assignComponent(this);
            connectors[1].assignComponent(this);
            connectors[0].setDllconnector(dllconnectors[0]);
            connectors[1].setDllconnector(dllconnectors[1]);
        }
    }
}
