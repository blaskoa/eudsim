﻿using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUIAnalogSwitch : GUICircuitComponent
{
    private string _name = "Switch";
    private AnalogSwitchEntity _analogSwitchEntity;
    private const bool DefaultState = true;

    public void SetName(string val)
    {
        _name = val;
    }
    
    //public bool TurnedOff
    //{
    //    get { return MyComponent.open; }
    //    set                                                 // GUI check - accept only true/ false
    //    {   //TO DO zistit ako funguje v DLL ten switch, lebo teraz to sice funguje ale ta logiga value == true/ false je postavena naopak
    //        if (MyComponent.open && value == false)
    //        {
    //            MyComponent.invert = true;
    //        }
    //        else if (MyComponent.open == false && value == true)
    //        {
    //            MyComponent.invert = true;
    //        }
    //    }
    //}

    public bool TurnedOff
    {
        get { return _analogSwitchEntity.TurnedOff; }
        set { _analogSwitchEntity.TurnedOff = value; }
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_analogSwitchEntity);
            return _analogSwitchEntity;
        }
        set
        {
            _analogSwitchEntity = (AnalogSwitchEntity) value;
            TransformFromEntity(_analogSwitchEntity);
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

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddBoolean("TurnedOffPropertyLabel", TurnedOff.ToString(), SetTurnedOff);
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "TurnedOff " + "</span>" + TurnedOff + " [true/ flase]" + " </p>";
    }


    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_analogSwitchEntity == null)
            {
                _analogSwitchEntity = new AnalogSwitchEntity {TurnedOff = DefaultState};
            }
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[7]++;
            _name += script.generatedIds[7].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        //todo toto ani boh neive ako funguje, budeme musiet spravit vlastny switch - mozno nejako len nespajat veci v obvode ak je vypnuty
        //todo a.k.a nieco podobne (toto asi bude padat)
        if (!_analogSwitchEntity.TurnedOff)
        {
        }

        AnalogSwitch analogSwitch = sim.Create<AnalogSwitch>();

        Connectors[0].DllConnector = analogSwitch.leadIn;
        Connectors[1].DllConnector = analogSwitch.leadOut;
    }

}
