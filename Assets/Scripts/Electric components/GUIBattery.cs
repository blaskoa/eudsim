using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;


public class GUIBattery : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public VoltageInput MyComponent;
    public Ground MyComponentGround;

    public double MaxVoltage
    {
        get { return MyComponent.maxVoltage; }
        set { MyComponent.maxVoltage = value; }   // GUI check - accept only positive integer
    }

    public void SetMaxVoltage(double val)
    {
        MaxVoltage = val;
    }

    public override void getProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("MaxVoltagePropertyLabel", MaxVoltage.ToString(), MaxVoltage.GetType().ToString(), SetMaxVoltage, false);
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