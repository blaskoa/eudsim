using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;


public class GUIAnalogSwitch : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public AnalogSwitch MyComponent;


    public bool TurnedOff
    {
        get { return MyComponent.open; }
        set                                                 // GUI check - accept only true/ false
        {   //TO DO zistit ako funguje v DLL ten switch, lebo teraz to sice funguje ale ta logiga value == true/ false je postavena naopak
            if (MyComponent.open && value == false)
            {
                MyComponent.invert = true;
            }
            else if (MyComponent.open == false && value == true)
            {
                MyComponent.invert = true;
            }
        }
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        TurnedOff = ((GUIAnalogSwitch) old).TurnedOff;
    }

    public void SetTurnedOff(bool val)
    {
        TurnedOff = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddBoolean("TurnedOffPropertyLabel", TurnedOff.ToString(), SetTurnedOff);
    }

    // Called during instantiation
    public void Awake()
    {
        if (this.CompareTag("ActiveItem"))
        {
            SetSimulationProp(GUICircuit.sim);
            Connectors = GetComponentsInChildren<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            SetDllConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("insertol som activeItem");
        DllConnectors = new Circuit.Lead[2];
        MyComponent = sim.Create<AnalogSwitch>();
        DllConnectors[0] = MyComponent.leadIn;
        DllConnectors[1] = MyComponent.leadOut;
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }

    public override void SetDllConnectors()
    {
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }
}
