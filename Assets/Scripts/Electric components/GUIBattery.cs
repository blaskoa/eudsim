using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIBattery : Component2
{
    public leader[] dllconnectors;
    public VoltageInput myComponent;
    Ground myComponentGround;

    public double getVoltageDelta()
    {
        return myComponent.getVoltageDelta();
    }

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        dllconnectors = new leader[2];
        myComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
        myComponentGround = GUICircuit.sim.Create<Ground>();
        dllconnectors[0] = myComponent.leadPos;
        dllconnectors[1] = myComponentGround.leadIn;

        //connectors = GetComponentsInChildren<Connector>();
        connectors[0] = gameObject.AddComponent<Connector>();
        connectors[1] = gameObject.AddComponent<Connector>();
        connectors[0].initialize();
        connectors[1].initialize();
        connectors[0].assignComponent((Component2)this);
        connectors[1].assignComponent((Component2)this);
        connectors[0].assignDllconnector(dllconnectors[0]);
        connectors[1].assignDllconnector(dllconnectors[1]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
