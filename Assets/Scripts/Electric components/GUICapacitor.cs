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
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            MyComponent = GUICircuit.sim.Create<CapacitorElm>();

            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;

            Connectors[0] = transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("MinusConnector").GetComponent<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllconnector(DllConnectors[0]);
            Connectors[1].SetDllconnector(DllConnectors[1]);
        }
    }
}
