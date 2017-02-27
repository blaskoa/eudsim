using UnityEngine;
using ClassLibrarySharpCircuit;
using System;
using System.Collections.Generic;
using System.Globalization;

public class GUIVoltmeter : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public Resistor MyComponent;

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddResult("VoltagePropertyLabel", MyComponent.getVoltageDelta().ToString(CultureInfo.InvariantCulture), "V");
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            SetSimulationProp(GUICircuit.sim);
            Connectors[0] = transform.FindChild("Connector1").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector2").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            SetDllConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");
        DllConnectors = new Circuit.Lead[2];
        MyComponent = sim.Create<Resistor>();
        DllConnectors[0] = MyComponent.leadIn;
        DllConnectors[1] = MyComponent.leadOut;
        MyComponent.resistance = Double.PositiveInfinity;
    }

    public override void SetDllConnectors()
    {
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }
}