using System;
using System.Globalization;
using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUILedDiode : GUICircuitComponent
{
    private LedDiodeEntity _ledDiodeEntity;
    private LEDElm _ledDiodeComponent;

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

        script.AddResult("CurrentPropertyLabel", _ledDiodeComponent.getCurrent().ToString(CultureInfo.InvariantCulture), "A");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Current " + "</span>" +
            Math.Round(_ledDiodeComponent.getCurrent(), 3).ToString(CultureInfo.InvariantCulture) + " [A]" + " </p>";
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
            //just for handling null references before we start the simulation
            _ledDiodeComponent = new LEDElm();

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        _ledDiodeComponent = sim.Create<LEDElm>();

        Connectors[0].DllConnector = _ledDiodeComponent.leadOut;
        Connectors[1].DllConnector = _ledDiodeComponent.leadIn;
    }
}
