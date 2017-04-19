using System;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;
using System.Collections.Generic;


public class GUIInductor : GUICircuitComponent
{
    private string _name = "Inductor";
    private InductorEntity _inductorEntity;
    private const double DefaultInductance = 50;
    private const bool DefaultIsTrapezoidal = false;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public double Inductance
    {
        get { return _inductorEntity.Inductance; }
        set { _inductorEntity.Inductance = value; }   // GUI check - accept only positive integer
    }

    public bool IsTrapezoidal
    {
        get { return _inductorEntity.IsTrapezoidal; }
        set { _inductorEntity.IsTrapezoidal = value; }   // GUI check - accept only true/ false
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_inductorEntity);
            return _inductorEntity;
        }
        set
        {
            _inductorEntity = (InductorEntity) value;
            TransformFromEntity(_inductorEntity);
        }
    }

    public void SetInductance(double val)
    {
        if (Inductance != val)
        {
            AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)(Inductance - val));
            prop.Add((float)0.0);                   // hodnoty ostatnych atributov sa nezmenia
            prop.Add((float)0.0);

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        Inductance = val;
        if (GameObject.Find("PlayToggle").GetComponent<UnityEngine.UI.Toggle>().isOn)
        {
            GameObject.Find("PlayButton").GetComponent<GUICircuit>().RunSimulation();
        }
    }

    public void SetTrapezoidal(bool val)
    {
        if (val != IsTrapezoidal)
        {
            AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)0.0);
            prop.Add((float)0.0);
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)1.0);                   // value as a sign of revertion of bool attribute

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        IsTrapezoidal = val;
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
        script.AddNumeric("InductancePropertyLabel", Inductance.ToString(), Inductance.GetType().ToString(), SetInductance, false);
        script.AddBoolean("TrapezoidalPropertyLabel", IsTrapezoidal.ToString(), SetTrapezoidal);
        script.AddResult("InductancePropertyLabel", "15.6", "Ohm");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Inductance " + "</span>" + Math.Round(Inductance, 3) + " [H]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Inductance = ((GUIInductor) old).Inductance;
        IsTrapezoidal = ((GUIInductor) old).IsTrapezoidal;
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_inductorEntity == null)
            {
                _inductorEntity = new InductorEntity {IsTrapezoidal = DefaultIsTrapezoidal, Inductance = DefaultInductance};
            }
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[4]++;
            _name += script.generatedIds[4].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        InductorElm inductorElm = sim.Create<InductorElm>();
        inductorElm.inductance = _inductorEntity.Inductance;
        inductorElm.isTrapezoidal = _inductorEntity.IsTrapezoidal;

        Connectors[0].DllConnector = inductorElm.leadIn;
        Connectors[1].DllConnector = inductorElm.leadOut;
    }

    public override void SetAllProperties(List<float> properties)
    {
        if (properties[1] == 1.0)
        {
            Inductance += properties[2];
        }
        if (properties[3] == 1.0)
        {
            Inductance += properties[4];
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