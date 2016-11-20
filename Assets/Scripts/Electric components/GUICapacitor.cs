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

    public override void getProperties()
    {
        EditObjectProperties.Add("CapacitancePropertyLabel", Capacitance.ToString());
    }

    public override void setProperties()
    {
        List<string> values = EditObjectProperties.Get();

        Capacitance = double.Parse(values[0]);
    }

    // Use this for initialization
    void Start()
    {
        DllConnectors = new Circuit.Lead[2];
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            MyComponent = GUICircuit.sim.Create<CapacitorElm>();

            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;

            Connectors[0] = transform.FindChild("Connector").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector").GetComponent<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
