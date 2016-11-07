using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIResistor : MonoBehaviour, ComponentInterface
{
    public leader[] connectors;
    Resistor myComponent;

    public double resistance
    {
        get { return myComponent.resistance; }
        set { myComponent.resistance = value; }
    }
    public double voltageDelta
    {
        get { return myComponent.getVoltageDelta(); }
    }
    public void inicializeGUIResistor()
    {
        connectors = new leader[2];
        myComponent = GUICircuit.sim.Create<Resistor>();
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
