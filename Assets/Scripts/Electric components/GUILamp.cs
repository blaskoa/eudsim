using System.Globalization;
using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUILamp : GUICircuitComponent
{
    private string _name = "Lamp";
    private LampEntity _lampEntity;
    private const float MaxIntensity = 8;
    private const float MaxSpotAngle = 179;
    private const float MaxCurrent = 100;
    private const float MinCurrent = 1;
    private const float MaxVoltage = 100;
    private const float MinVoltage = 1;
    private Lamp _lamp;

    void Update()
    {
        //if is there current and voltage in the active item lamp
        if (CompareTag("ActiveItem")
            && _lamp != null
            && Mathf.Abs((float)_lamp.getCurrent()) > 0
            && Mathf.Abs((float)_lamp.getVoltageDelta()) > 0
            )
        {
            float current = Mathf.Abs((float)_lamp.getCurrent());
            float voltage = Mathf.Abs((float)_lamp.getVoltageDelta());
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
           // Debug.Log(current + "   " + voltage);
            light.intensity = MaxIntensity * voltage / MaxVoltage;
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

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[2]++;
            _name += script.generatedIds[2].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        _lamp = sim.Create<Lamp>();

        Connectors[0].DllConnector = _lamp.leadIn;
        Connectors[1].DllConnector = _lamp.leadOut;
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
