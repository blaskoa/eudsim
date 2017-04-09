using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Globalization;
using Assets.Scripts.Entities;

public class GUIAmpermeter : GUICircuitComponent
{
    private string _name = "Ampermeter";
    public Resistor ResistorComponent;
    private AmpermeterEntity _ampermeterEntity;
    private const double MinimalResistance = 0.01;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_ampermeterEntity);
            return _ampermeterEntity;
        }
        set
        {
            _ampermeterEntity = (AmpermeterEntity) value;
            TransformFromEntity(_ampermeterEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddResult("CurrentPropertyLabel", ResistorComponent.getCurrent().ToString(CultureInfo.InvariantCulture), "A");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Measured current " + "</span>" +
            ResistorComponent.getCurrent().ToString(CultureInfo.InvariantCulture) + " [A]" + " </p>";
    }

    // Called during instantiation
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_ampermeterEntity == null)
            {
                _ampermeterEntity = new AmpermeterEntity();
            }
            //just for handling null references before we start the simulation
            ResistorComponent = new Resistor();

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[1]++;
            _name += script.generatedIds[1].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        ResistorComponent = sim.Create<Resistor>();
        Connectors[0].DllConnector = ResistorComponent.leadIn;
        Connectors[1].DllConnector = ResistorComponent.leadOut;
        ResistorComponent.resistance = MinimalResistance;
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
