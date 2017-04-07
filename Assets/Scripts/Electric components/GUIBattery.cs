using System;
using System.Linq;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;
using System.Collections.Generic;


public class GUIBattery : GUICircuitComponent
{
    private BatteryEntity _batteryEntity;

    private const double DefaultVoltage = 10;
    
    public double MaxVoltage
    {
        get { return _batteryEntity.MaxVoltage; }
        set { _batteryEntity.MaxVoltage = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_batteryEntity);
            return _batteryEntity;
        }
        set
        {
            _batteryEntity = (BatteryEntity)value;
            TransformFromEntity(_batteryEntity);
        }
    }

    public void SetMaxVoltage(double val)
    {
        if (MaxVoltage != val)
        {
           AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)(MaxVoltage - val));

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        MaxVoltage = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("MaxVoltagePropertyLabel", MaxVoltage.ToString(), MaxVoltage.GetType().ToString(), SetMaxVoltage, false);
    }

    public override void SetAllProperties(List<float> properties)
    {
        if (properties[1] == 1.0)
        {
            MaxVoltage += properties[2];
        }
    }


    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Voltage " + "</span>" + MaxVoltage + " [V]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        MaxVoltage = ((GUIBattery) old).MaxVoltage;
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_batteryEntity == null)
            {
                _batteryEntity = new BatteryEntity { MaxVoltage = DefaultVoltage };
            }

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        VoltageInput voltageInput = sim.Create<VoltageInput>(Voltage.WaveType.DC);
        voltageInput.maxVoltage = _batteryEntity.MaxVoltage;

        Ground ground = sim.Create<Ground>();
        Connectors[0].DllConnector = voltageInput.leadPos;
        Connectors[1].DllConnector = ground.leadIn;
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