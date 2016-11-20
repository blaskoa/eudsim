using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUILamp : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public Lamp MyComponent;

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<Lamp>();
            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;
     
            Connectors[0] = this.transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = this.transform.FindChild("MinusConnector").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
