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
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            DLLCconnectors = new leader[2];
            MyComponent = GUICircuit.sim.Create<InductorElm>();
            DLLCconnectors[0] = MyComponent.leadIn;
            DLLCconnectors[1] = MyComponent.leadOut;

            Connectors[0] = transform.FindChild("Connector1").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector2").GetComponent<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllconnector(DLLCconnectors[0]);
            Connectors[1].SetDllconnector(DLLCconnectors[1]);
        }
    }
}
