using System.Globalization;
using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUILamp : GUICircuitComponent
{
    private string _name = "Lamp";
    private LampEntity _lampEntity;
    public Resistor ResistorComponent;
    private const double MinimalResistance = 0.01;
    private const float MaxIntensity = 8;
    private const float MaxSpotAngle = 179;
    private const float MaxCurrent = 10000;
    private const float MinCurrent = 10;
    private const float MaxVoltage = 100;
    private const float MinVoltage = 1;

    void Update()
    {
        //if is there current and voltage in the active item lamp
        if (CompareTag("ActiveItem") 
            && Mathf.Abs((float)ResistorComponent.getCurrent()) > 0
            && Mathf.Abs((float)ResistorComponent.getVoltageDelta()) > 0)
        {
            float current = Mathf.Abs((float)ResistorComponent.getCurrent());
            float voltage = Mathf.Abs((float)ResistorComponent.getVoltageDelta());
            Light light = this.gameObject.GetComponent<Light>();

            // show light
            light.enabled = true; 
      
            //limitation od maximal current and voltage
            if (current > MaxCurrent) 
            {
                current = MaxCurrent;
            }

            if (current < MinCurrent)
            {
                current = MinCurrent;
            }

            if (voltage > MaxVoltage)
            {
                voltage = MaxVoltage;
            }

            if (voltage < MinVoltage)
            {
                voltage = MinVoltage;
            }

            //dinamically set the Light options in scene
            light.intensity = MaxIntensity * current / MaxCurrent;
            light.spotAngle = MaxSpotAngle * voltage / MaxVoltage;
        }
    }

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
    public override void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            id = idCounter++;
            if (_lampEntity == null)
            {
                _lampEntity = new LampEntity();
            }
            ResistorComponent = new Resistor();

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[2]++;
            _name += script.generatedIds[2].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Lamp lamp = sim.Create<Lamp>();

        Connectors[0].DllConnector = lamp.leadIn;
        Connectors[1].DllConnector = lamp.leadOut;

        //to get info of current and voltage in lamp
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
