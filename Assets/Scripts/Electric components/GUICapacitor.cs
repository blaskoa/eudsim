﻿using Assets.Scripts.Entities;
using UnityEngine;
using ClassLibrarySharpCircuit;

public class GUICapacitor : GUICircuitComponent
{
    private string _name = "Capacitor";
    private CapacitorEntity _capacitorEntity;
    private const double DefaultCapacitance = 50;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public double Capacitance
    {
        get { return _capacitorEntity.Capacitance; }
        set { _capacitorEntity.Capacitance = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_capacitorEntity);
            return _capacitorEntity;
        }
        set
        {
            _capacitorEntity = (CapacitorEntity) value;
            TransformFromEntity(_capacitorEntity);
        }
    }


    public void SetCapacitance(double val)
    {
        Capacitance = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddNumeric("CapacitancePropertyLabel", Capacitance.ToString(), Capacitance.GetType().ToString(), SetCapacitance, false);
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Capacitance " + "</span>" + Capacitance + " [uF]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Capacitance = ((GUICapacitor)old).Capacitance;
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_capacitorEntity == null)
            {
                _capacitorEntity = new CapacitorEntity {Capacitance = DefaultCapacitance};
            }
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[3]++;
            _name += script.generatedIds[3].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        CapacitorElm capacitorElm = sim.Create<CapacitorElm>();

        capacitorElm.capacitance = _capacitorEntity.Capacitance;
        Connectors[0].DllConnector = capacitorElm.leadIn;
        Connectors[1].DllConnector = capacitorElm.leadOut;
    }
}
