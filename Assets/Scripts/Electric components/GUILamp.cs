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
