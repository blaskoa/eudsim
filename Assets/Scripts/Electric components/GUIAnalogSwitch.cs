using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;
using System.Collections.Generic;

public class GUIAnalogSwitch : GUICircuitComponent
{
    private string _name = "Switch";
    private AnalogSwitchEntity _analogSwitchEntity;
    private SwitchSPST _analogSwitchComponent;
    private const bool DefaultState = false;

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
        set {
            if (value != _analogSwitchEntity.TurnedOff)
            {
                AttChange change = new AttChange();

                List<float> prop = new List<float>();
                prop.Add((float)this.GetId());
                prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
                prop.Add((float)1.0);                   // value as a sign of revertion of bool attribute

                change.SetChange(prop);
                UndoAction undoAction = new UndoAction();
                undoAction.AddChange(change);

                GUICircuitComponent.globalUndoList.AddUndo(undoAction);
            }

            _analogSwitchEntity.TurnedOff = value;
            if (_analogSwitchEntity.TurnedOff == true)
            {
                if (_analogSwitchComponent.IsOpen == true)
                {
                    _analogSwitchComponent.toggle();
                    Debug.Log("Zavrel som switch");
                }
            }
            else if (_analogSwitchEntity.TurnedOff == false)
            {
                if (_analogSwitchComponent.IsOpen == false)
                {
                    _analogSwitchComponent.toggle();
                    Debug.Log("Otvoril som switch");
                }
            }
          
            if (GameObject.Find("PlayToggle").GetComponent<UnityEngine.UI.Toggle>().isOn)
            {
                GameObject.Find("PlayButton").GetComponent<GUICircuit>().RunSimulation();
            }
        }
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
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_analogSwitchEntity == null)
            {
                _analogSwitchEntity = new AnalogSwitchEntity {TurnedOff = DefaultState};
            }
            _analogSwitchComponent = new SwitchSPST();
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[7]++;
            _name += script.generatedIds[7].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        _analogSwitchComponent = sim.Create<SwitchSPST>();

        Connectors[0].DllConnector = _analogSwitchComponent.leadA;
        Connectors[1].DllConnector = _analogSwitchComponent.leadB;
    }

    public override void SetAllProperties(List<float> properties)
    {
        if ((properties[1] == 1.0) && (properties[2] == 1.0))  // index 1 and revertion value 1 == true
        {
            if (TurnedOff == true)
            {
                TurnedOff = false;
            }
            else
            {
                TurnedOff = true;
            }
        }
    }
    public override void SetId(int id)
    {
        this.id = id;
    }

    public override int GetId()
    {
        return id;
    }
}
