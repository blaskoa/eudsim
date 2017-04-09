using Assets.Scripts.Entities;
using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;

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
        if (Capacitance != val)
        {
            AttChange change = new AttChange();

            List<float> prop = new List<float>();
            prop.Add((float)this.GetId());
            prop.Add((float)1.0);                   // value of attribute index,, not nessesary but just frameword for case of adding attributes
            prop.Add((float)(Capacitance - val));

            change.SetChange(prop);
            UndoAction undoAction = new UndoAction();
            undoAction.AddChange(change);

            GUICircuitComponent.globalUndoList.AddUndo(undoAction);
        }

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

    public override void SetAllProperties(List<float> properties)
    {
        if (properties[1] == 1.0)
        {
            Capacitance += properties[2];
        }
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Capacitance = ((GUICapacitor)old).Capacitance;
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
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
        CapacitorElm capacitorElm = sim.Create<CapacitorElm>();

        capacitorElm.capacitance = _capacitorEntity.Capacitance;
        Connectors[0].DllConnector = capacitorElm.leadIn;
        Connectors[1].DllConnector = capacitorElm.leadOut;
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
