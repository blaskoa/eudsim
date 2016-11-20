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

            Connectors = GetComponentsInChildren<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllconnector(dllconnectors[0]);
            Connectors[1].SetDllconnector(dllconnectors[1]);
        }
    }
}
