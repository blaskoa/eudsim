using System;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;
using System.Collections.Generic;

public class GUIResistor : GUICircuitComponent
{
    private string _name = "Resistor";
    private ResistorEntity _resistorEntity;

    private const double DefaultResistance = 50;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public double Resistance
    {
        get { return _resistorEntity.Resistance; }
        set { _resistorEntity.Resistance = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_resistorEntity);
            return _resistorEntity;
        }
        set
        {
            _resistorEntity = (ResistorEntity) value;
            TransformFromEntity(_resistorEntity);
        }
    }

    public void SetResistance(double val)
    {
        if (Resistance != val)
        {
            AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)(Resistance - val));

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        Resistance = val;
        if (GameObject.Find("PlayToggle").GetComponent<UnityEngine.UI.Toggle>().isOn)
        {
            GameObject.Find("PlayButton").GetComponent<GUICircuit>().RunSimulation();
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddNumeric("ResistancePropertyLabel", Resistance.ToString(), Resistance.GetType().ToString(), SetResistance, true, 0, 150.6f);
    }

    public override void SetAllProperties(List<float> properties)
    {
        if (properties[1] == 1.0)
        {
            Resistance += properties[2];
        }
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Resistance " + "</span>" + Math.Round(Resistance, 3) + " [Ohm]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Resistance = ((GUIResistor) old).Resistance;
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_resistorEntity == null)
            {
                _resistorEntity = new ResistorEntity {Resistance = DefaultResistance};
            }

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[6]++;
            _name += script.generatedIds[6].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Resistor resistor = sim.Create<Resistor>();
        resistor.resistance = _resistorEntity.Resistance;

        Connectors[0].DllConnector = resistor.leadIn;
        Connectors[1].DllConnector = resistor.leadOut;
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
