using System;
using System.Linq;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;


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
        MaxVoltage = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("MaxVoltagePropertyLabel", MaxVoltage.ToString(), MaxVoltage.GetType().ToString(), SetMaxVoltage, false);
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        MaxVoltage = ((GUIBattery) old).MaxVoltage;
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
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
}