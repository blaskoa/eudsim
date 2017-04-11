using System.Globalization;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUILedDiode : GUICircuitComponent
{
    private LedDiodeEntity _ledDiodeEntity;

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_ledDiodeEntity);
            return _ledDiodeEntity;
        }
        set
        {
            _ledDiodeEntity = (LedDiodeEntity) value;
            TransformFromEntity(_ledDiodeEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddResult("CurrentPropertyLabel", _ledDiodeEntity.Current.ToString(CultureInfo.InvariantCulture), "A");
        script.AddResult("VoltagePropertyLabel", _ledDiodeEntity.Voltage.ToString(CultureInfo.InvariantCulture), "V");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Measured Voltage " + "</span>" + _ledDiodeEntity.Voltage + " [V]" + " </p>" +
        "<p><span class=\"field-title\">" + "Measured current " + "</span>" + _ledDiodeEntity.Current.ToString(CultureInfo.InvariantCulture) + " [A]" + " </p>";

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
            if (_ledDiodeEntity == null)
            {
                _ledDiodeEntity = new LedDiodeEntity();
            }
            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        LEDElm ledDiode = sim.Create<LEDElm>();

        Connectors[0].DllConnector = ledDiode.leadIn;
        Connectors[1].DllConnector = ledDiode.leadOut;
    }
}
