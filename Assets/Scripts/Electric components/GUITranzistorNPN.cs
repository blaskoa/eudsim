using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUITranzistorNPN : GUICircuitComponent
{
    private TranzistorNPNEntity _transistorEntity;

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_transistorEntity);
            return _transistorEntity;
        }
        set
        {
            _transistorEntity = (TranzistorNPNEntity) value;
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
        return "<p><span class=\"field-title\">" + "Type " + "</span>" + " NPN" + " </p>";
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_transistorEntity == null)
            {
                _transistorEntity = new TranzistorNPNEntity();
            }

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        Transistor transistor = sim.Create<Transistor>();
        transistor.IsPNP = false;

        Connectors[0].DllConnector = transistor.leadCollector;
        Connectors[1].DllConnector = transistor.leadEmitter;
        Connectors[2].DllConnector = transistor.leadBase;
    }
}
