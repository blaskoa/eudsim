using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUILamp : GUICircuitComponent
{
    private string _name = "Lamp";
    private LampEntity _lampEntity;
    
    public void SetName(string val)
    {
        _name = val;
    }
    
    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_lampEntity);
            return _lampEntity;
        }
        set
        {
            _lampEntity = (LampEntity) value;
            TransformFromEntity(_lampEntity);
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
        return "<p><span class=\"field-title\">" + "GUI Lamp " + "</span>" + "Nothing to show" + " [N/A]" + " </p>";
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_lampEntity == null)
            {
                _lampEntity = new LampEntity();
            }

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[2]++;
            _name += script.generatedIds[2].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("insertol som activeItem");
        Lamp lamp = sim.Create<Lamp>();

        Connectors[0].DllConnector = lamp.leadIn;
        Connectors[1].DllConnector = lamp.leadOut;
    }
}
