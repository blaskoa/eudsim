using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUIResistor : GUICircuitComponent
{
    private ResistorEntity _resistorEntity;

    private const double DefaultResistance = 50;

    public double Resistance
    {
        get { return _resistorEntity.Resistance; }
        set { _resistorEntity.Resistance = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_resistorEntity);
            return _resistorEntity;
        }
        set
        {
            _resistorEntity = (ResistorEntity) value;
            TransformFromEntity(_resistorEntity);
        }
    }

    public void SetResistance(double val)
    {
        Resistance = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("ResistancePropertyLabel", Resistance.ToString(), Resistance.GetType().ToString(), SetResistance, true, -15.4f, 150.6f);
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Resistance = ((GUIResistor) old).Resistance;
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_resistorEntity == null)
            {
                _resistorEntity = new ResistorEntity {Resistance = DefaultResistance};
            }

            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        Resistor resistor = sim.Create<Resistor>();
        resistor.resistance = _resistorEntity.Resistance;

        Connectors[0].DllConnector = resistor.leadIn;
        Connectors[1].DllConnector = resistor.leadOut;
    }
}
