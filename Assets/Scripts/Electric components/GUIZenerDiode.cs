using System.Globalization;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUIZenerDiode : GUICircuitComponent
{
    private string _name = "Zener Diode";
    private ZenerDiodeEntity _zenerEntity;
    private ZenerElm diodeComponent;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_zenerEntity);
            return _zenerEntity;
        }
        set
        {
            _zenerEntity = (ZenerDiodeEntity) value;
            TransformFromEntity(_zenerEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddResult("CurrentPropertyLabel", diodeComponent.getCurrent().ToString(CultureInfo.InvariantCulture), "A");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Current " + "</span>" +
            diodeComponent.getCurrent().ToString(CultureInfo.InvariantCulture) + " [A]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_zenerEntity == null)
            {
                _zenerEntity = new ZenerDiodeEntity();
            }
            diodeComponent = new ZenerElm();

            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[9]++;
            _name += script.generatedIds[9].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        diodeComponent = sim.Create<ZenerElm>();

        Connectors[0].DllConnector = diodeComponent.leadIn;
        Connectors[1].DllConnector = diodeComponent.leadOut;
    }
}
