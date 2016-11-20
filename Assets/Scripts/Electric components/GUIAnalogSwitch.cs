using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUIAnalogSwitch : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public AnalogSwitch MyComponent;

    // Use this for initialization
    void Start()
    {
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<AnalogSwitch>();
            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;

            Connectors = GetComponentsInChildren<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
