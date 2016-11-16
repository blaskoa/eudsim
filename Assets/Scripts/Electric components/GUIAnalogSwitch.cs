using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;
using System;

public class GUIAnalogSwitch : Component2
{
    public leader[] dllconnectors;
    public AnalogSwitch MyComponent;


    public bool Vypnuty
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

    public override void getProperties()
    {
        EditObjectProperties.Add("Vypnuty", Vypnuty.ToString());
    }

    public override void setProperties()
    {
        
    }
    
    // Use this for initialization
    void Start()
    {
        dllconnectors = new leader[2];
        MyComponent = GUICircuit.sim.Create<AnalogSwitch>();
        dllconnectors[0] = MyComponent.leadIn;
        dllconnectors[1] = MyComponent.leadOut;

        connectors = GetComponentsInChildren<Connector>();

        connectors[0].setConnectedConnectors();
        connectors[1].setConnectedConnectors();
        connectors[0].assignComponent(this);
        connectors[1].assignComponent(this);
        connectors[0].setDllconnector(dllconnectors[0]);
        connectors[1].setDllconnector(dllconnectors[1]);
    }
}
