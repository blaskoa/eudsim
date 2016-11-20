using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUILamp : Component2
{
    public leader[] DLLConnectors;
    public Lamp MyComponent;

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            DLLConnectors = new leader[2];
            MyComponent = GUICircuit.sim.Create<Lamp>();
            DLLConnectors[0] = MyComponent.leadIn;
            DLLConnectors[1] = MyComponent.leadOut;
     
            Connectors[0] = this.transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = this.transform.FindChild("MinusConnector").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllconnector(DLLConnectors[0]);
            Connectors[1].SetDllconnector(DLLConnectors[1]);
        }
    }
}
