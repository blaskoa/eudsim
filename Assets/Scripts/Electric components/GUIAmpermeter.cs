using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Globalization;
using Assets.Scripts.Entities;

public class GUIAmpermeter : GUICircuitComponent
{
    public Resistor ResistorComponent;
    private AmpermeterEntity _ampermeterEntity;
    private const double MinimalResistance = 0.01;

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

        script.AddResult("CurrentPropertyLabel", ResistorComponent.getCurrent().ToString(CultureInfo.InvariantCulture), "A");
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_ampermeterEntity == null)
            {
                _ampermeterEntity = new AmpermeterEntity();
            }
            //just for handling null references before we start the simulation
            ResistorComponent = new Resistor();

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        ResistorComponent = sim.Create<Resistor>();
        Connectors[0].DllConnector = ResistorComponent.leadIn;
        Connectors[1].DllConnector = ResistorComponent.leadOut;
        ResistorComponent.resistance = MinimalResistance;
    }
}
