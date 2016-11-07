using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIBattery : MonoBehaviour, ComponentInterface
{
    public leader[] connectors;
    VoltageInput myComponent;
    Ground myComponentGround;

    public double voltageDelta
    {
        get { return myComponent.getVoltageDelta(); }
        set { myComponent.maxVoltage = value; }
    }

    public void inicializeGUIBattery()
    {
        connectors = new leader[2];
        myComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
        myComponentGround = GUICircuit.sim.Create<Ground>();
        connectors[0] = myComponent.leadPos;
        connectors[1] = myComponentGround.leadIn;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
