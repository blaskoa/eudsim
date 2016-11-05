using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIResistor : MonoBehaviour, ComponentInterface
{
    public leader[] connectors = new leader[2];
    Resistor myComponent = GUICircuit.sim.Create<Resistor>();

    public double getVoltageDelta()
    {
        return myComponent.getVoltageDelta();
    }
    public GUIResistor()
    {
        connectors[0] = myComponent.leadIn;
        connectors[1] = myComponent.leadOut;
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
