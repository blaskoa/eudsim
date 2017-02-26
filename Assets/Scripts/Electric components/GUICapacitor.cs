using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;

public class GUICapacitor : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public CapacitorElm MyComponent;

    public double Capacitance
    {
        get { return MyComponent.capacitance; }
        set { MyComponent.capacitance = value; }   // GUI check - accept only positive integer
    }

    public void SetCapacitance(double val)
    {
        Capacitance = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("CapacitancePropertyLabel", Capacitance.ToString(), Capacitance.GetType().ToString(), SetCapacitance, false);
    }

    public override void CopyValues(GUICircuitComponent old)
    {
        Capacitance = ((GUICapacitor)old).Capacitance;
    }

    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            SetSimulationProp(GUICircuit.sim);
            Connectors[0] = transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("MinusConnector").GetComponent<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            SetDllConnectors();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");
        MyComponent = sim.Create<CapacitorElm>();
        DllConnectors = new Circuit.Lead[2];
        DllConnectors[0] = MyComponent.leadIn;
        DllConnectors[1] = MyComponent.leadOut;
    }

    public override void SetDllConnectors()
    {
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }
}
