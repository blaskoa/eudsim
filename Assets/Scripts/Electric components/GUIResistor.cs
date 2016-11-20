using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIResistor : Component2
{
    public leader[] DLLConnectors;
    public Resistor MyComponent;

    public double GetVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }

    // Use this for initialization
    public void Start()
    {
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            DLLConnectors = new leader[2];
            MyComponent = GUICircuit.sim.Create<Resistor>();
            DLLConnectors[0] = MyComponent.leadIn;
            DLLConnectors[1] = MyComponent.leadOut;
            
            Connectors[0] = transform.FindChild("Connector1").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector2").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllconnector(DLLConnectors[0]);
            Connectors[1].SetDllconnector(DLLConnectors[1]);
        }
    }
}
