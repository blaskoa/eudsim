using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUILamp : GUICircuitComponent
{
    private LampEntity _lampEntity;
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
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Lamp lamp = sim.Create<Lamp>();

        Connectors[0].DllConnector = lamp.leadIn;
        Connectors[1].DllConnector = lamp.leadOut;
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
