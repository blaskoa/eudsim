using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUITransistorPNP : GUICircuitComponent
{
    private string _name = "Transistor PNP";
    private TransistorPNPEntity _transistorEntity;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_transistorEntity);
            return _transistorEntity;
        }
        set
        {
            _transistorEntity = (TransistorPNPEntity) value;
            TransformFromEntity(_transistorEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Type " + "</span>" + "PNP" + " </p>";
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_transistorEntity == null)
            {
                _transistorEntity = new TransistorPNPEntity();
            }
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[10]++;
            _name += script.generatedIds[10].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        Transistor transistor = sim.Create<Transistor>();
        transistor.IsPNP = true;

        Connectors[0].DllConnector = transistor.leadCollector;
        Connectors[1].DllConnector = transistor.leadEmitter;
        Connectors[2].DllConnector = transistor.leadBase;
    }
}
