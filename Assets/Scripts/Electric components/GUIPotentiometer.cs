using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;
using System.Collections.Generic;

public class GUIPotentiometer : GUICircuitComponent
{
    private string _name = "Potentiometer";
    private PotentiometerEntity _potentiometerEntity;

    private const double DefaultPosition = 0.5;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public double Position
    {
        get { return _potentiometerEntity.Position; }
        set { _potentiometerEntity.Position = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_potentiometerEntity);
            return _potentiometerEntity;
        }
        set
        {
            _potentiometerEntity = (PotentiometerEntity) value;
            TransformFromEntity(_potentiometerEntity);
        }
    }

    public void SetResistance(double val)
    {
        if (Position != val)
        {
            AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)(Position - val));

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }
        Position = val;
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
        script.AddNumeric("ResistancePropertyLabel", Position.ToString(), Position.GetType().ToString(), SetResistance, true, -15.4f, 150.6f);
    }

    public override void SetAllProperties(List<float> properties)
    {
        if (properties[1] == 1.0)
        {
            Position += properties[2];
        }
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Resistance " + "</span>" + Position + " [%]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Position = ((GUIPotentiometer) old).Position;
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_potentiometerEntity == null)
            {
                _potentiometerEntity = new PotentiometerEntity { Position = DefaultPosition};
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
        Potentiometer potentiometer = sim.Create<Potentiometer>();
        potentiometer.position = _potentiometerEntity.Position;

        Connectors[0].DllConnector = potentiometer.leadVoltage;
        Connectors[1].DllConnector = potentiometer.leadIn;
        Connectors[2].DllConnector = potentiometer.leadOut;
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
