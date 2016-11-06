using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIBattery : MonoBehaviour, ComponentInterface
{
    public leader[] connectors = new leader[2];
    VoltageInput myComponent = GUICircuit.sim.Create<VoltageInput>(Voltage.WaveType.DC);
    Ground myComponentGround = GUICircuit.sim.Create<Ground>();

    public double getVoltageDelta()
    {
        return myComponent.getVoltageDelta();
    }
    public GUIBattery()
    {
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
