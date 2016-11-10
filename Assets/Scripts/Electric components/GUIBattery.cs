using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIBattery : Component2
{
    public leader[] DLLConnectors;
    public VoltageInput MyComponent;
    public Ground MyComponentGround;

    public double GetVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        DLLConnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
        MyComponentGround = GUICircuit.sim.Create<Ground>();
        DLLConnectors[0] = MyComponent.leadPos;
        DLLConnectors[1] = MyComponentGround.leadIn;

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

    // Update is called once per frame
    void Update()
    {

    }
}
