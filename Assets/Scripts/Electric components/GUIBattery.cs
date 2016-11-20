using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUIBattery : Component2
{
    public Circuit.Lead[] DllConnectors;
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
            Debug.Log("activeItem inserted");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
            MyComponentGround = GUICircuit.sim.Create<Ground>();
            DllConnectors[0] = MyComponent.leadPos;
            DllConnectors[1] = MyComponentGround.leadIn;

            Connectors[0] = transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("MinusConnector").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
