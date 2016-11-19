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
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            DLLConnectors = new leader[2];
            MyComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
            MyComponentGround = GUICircuit.sim.Create<Ground>();
            DLLConnectors[0] = MyComponent.leadPos;
            DLLConnectors[1] = MyComponentGround.leadIn;

            connectors[0] = this.transform.FindChild("PlusConnector").GetComponent<Connector>();
            connectors[1] = this.transform.FindChild("MinusConnector").GetComponent<Connector>();
            connectors[0].setConnectedConnectors();
            connectors[1].setConnectedConnectors();
            connectors[0].assignComponent(this);
            connectors[1].assignComponent(this);
            connectors[0].setDllconnector(DLLConnectors[0]);
            connectors[1].setDllconnector(DLLConnectors[1]);
        }
    }
}
