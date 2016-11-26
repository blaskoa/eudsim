using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;


public class GUIInductor : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public InductorElm MyComponent;

    public double Inductance
    {
        get { return MyComponent.inductance; }
        set { MyComponent.inductance = value; }   // GUI check - accept only positive integer
    }

    public void SetInductance(double val)
    {
        Inductance = val;
    }

    public bool IsTrapezoidal
    {
        get { return MyComponent.isTrapezoidal; }
        set { MyComponent.isTrapezoidal = value; }   // GUI check - accept only true/ false
    }

    public void SetTrapezoidal(bool val)
    {
        IsTrapezoidal = val;
    }

    public override void getProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("InductancePropertyLabel", Inductance.ToString(), Inductance.GetType().ToString(), SetInductance, false);
        script.AddBoolean("TrapezoidalPropertyLabel", IsTrapezoidal.ToString(), SetTrapezoidal);
    }

    // Use this for initialization
    void Start()
    {
        if (CompareTag("ActiveItem"))
        {
            Debug.Log("activeItem inserted");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<InductorElm>();
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
