using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUITranzistorPNP : GUICircuitComponent
{
    private TranzistorPNPEntity _transistorEntity;

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_transistorEntity);
            return _transistorEntity;
        }
        set
        {
            _transistorEntity = (TranzistorPNPEntity) value;
            TransformFromEntity(_transistorEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();
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
                _transistorEntity = new TranzistorPNPEntity();
            }

            SetAndInitializeConnectors();
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
