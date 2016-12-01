using UnityEngine;
using ClassLibrarySharpCircuit;
using System;
using System.Collections.Generic;

public class GUIResistor : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public Resistor MyComponent;

    public double Resistance
    {
        get { return MyComponent.resistance; }
        set { MyComponent.resistance = value; }   // GUI check - accept only positive integer
    }

    public void SetResistance(double val)
    {
        Resistance = val;
    }

    public double GetVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("ResistancePropertyLabel", Resistance.ToString(), Resistance.GetType().ToString(), SetResistance, true, -15.4f, 150.6f);
    }

    // Use this for initialization
    public void Start()
    {
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<Resistor>();
            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;
            
            Connectors[0] = transform.FindChild("Connector1").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector2").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
