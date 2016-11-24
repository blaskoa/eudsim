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

    public void SetTurnedOff(bool val)
    {
        TurnedOff = val;
    }

    public override void getProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddBoolean("TurnedOffPropertyLabel", TurnedOff.ToString(), SetTurnedOff);
    }
    
    // Use this for initialization
    void Start()
    {
        if (this.CompareTag("ActiveItem"))
        {
            Debug.Log("insertol som activeItem");
            DllConnectors = new Circuit.Lead[2];
            MyComponent = GUICircuit.sim.Create<AnalogSwitch>();
            DllConnectors[0] = MyComponent.leadIn;
            DllConnectors[1] = MyComponent.leadOut;

            Connectors = GetComponentsInChildren<Connector>();

            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            Connectors[0].SetDllConnector(DllConnectors[0]);
            Connectors[1].SetDllConnector(DllConnectors[1]);
        }
    }
}
