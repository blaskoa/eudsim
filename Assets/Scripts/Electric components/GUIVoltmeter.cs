﻿using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Globalization;
using Assets.Scripts.Entities;

public class GUIVoltmeter : GUICircuitComponent
{
    private string _name = "Voltmeter";
    public Resistor ResistorComponent;
    private VoltmeterEntity _voltmeterEntity;
    private const double MaximumResistance = double.PositiveInfinity;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_voltmeterEntity);
            return _voltmeterEntity;
        }
        set
        {
            _voltmeterEntity = (VoltmeterEntity) value;
            TransformFromEntity(_voltmeterEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddResult("VoltagePropertyLabel", ResistorComponent.getVoltageDelta().ToString(CultureInfo.InvariantCulture), "V");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Measured Voltage " + "</span>" + ResistorComponent.getVoltageDelta() + " [V]" + " </p>";
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_voltmeterEntity == null)
            {
                _voltmeterEntity = new VoltmeterEntity();
            }
            ResistorComponent = new Resistor();

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[8]++;
            _name += script.generatedIds[8].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");
        ResistorComponent = sim.Create<Resistor>();
        Connectors[0].DllConnector = ResistorComponent.leadIn;
        Connectors[1].DllConnector = ResistorComponent.leadOut;
        ResistorComponent.resistance = MaximumResistance;
    }
}