using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIResistor : Component2
{
    public leader[] DLLConnectors;
    public Resistor MyComponent;

    public double getVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }
  
    // Use this for initialization
    public void Start()
    {
        DLLConnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<Resistor>();
        DLLConnectors[0] = MyComponent.leadIn;
        DLLConnectors[1] = MyComponent.leadOut;

        //connectors = GetComponentsInChildren<Connector>();
        connectors[0] = gameObject.AddComponent<Connector>();
        connectors[1] = gameObject.AddComponent<Connector>();
        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(DLLConnectors[0]);
        connectors[1].assignDllconnector(DLLConnectors[1]);
    }
}
