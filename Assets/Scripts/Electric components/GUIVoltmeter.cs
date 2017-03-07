using UnityEngine;
using ClassLibrarySharpCircuit;
using System.Globalization;
using Assets.Scripts.Entities;

public class GUIVoltmeter : GUICircuitComponent
{
    public Resistor ResistorComponent;
    private VoltmeterEntity _voltmeterEntity;
    private const double MaximumResistance = double.PositiveInfinity;

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_voltmeterEntity);
            return _voltmeterEntity;
        }
        set
        {
            _voltmeterEntity = (VoltmeterEntity) value;
            TransformFromEntity(_voltmeterEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddResult("VoltagePropertyLabel", ResistorComponent.getVoltageDelta().ToString(CultureInfo.InvariantCulture), "V");
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_voltmeterEntity == null)
            {
                _voltmeterEntity = new VoltmeterEntity();
            }
            ResistorComponent = new Resistor();

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");
        ResistorComponent = sim.Create<Resistor>();
        Connectors[0].DllConnector = ResistorComponent.leadIn;
        Connectors[1].DllConnector = ResistorComponent.leadOut;
        ResistorComponent.resistance = MaximumResistance;
    }
}