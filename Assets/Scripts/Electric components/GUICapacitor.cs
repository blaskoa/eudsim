using Assets.Scripts.Entities;
using UnityEngine;
using ClassLibrarySharpCircuit;

public class GUICapacitor : GUICircuitComponent
{
    private CapacitorEntity _capacitorEntity;
    private const double DefaultCapacitance = 50;

    public double Capacitance
    {
        get { return _capacitorEntity.Capacitance; }
        set { _capacitorEntity.Capacitance = value; }   // GUI check - accept only positive integer
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_capacitorEntity);
            return _capacitorEntity;
        }
        set
        {
            _capacitorEntity = (CapacitorEntity) value;
            TransformFromEntity(_capacitorEntity);
        }
    }


    public void SetCapacitance(double val)
    {
        Capacitance = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("CapacitancePropertyLabel", Capacitance.ToString(), Capacitance.GetType().ToString(), SetCapacitance, false);
    }

    // Use this for initialization
    public void Start()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_capacitorEntity == null)
            {
                _capacitorEntity = new CapacitorEntity {Capacitance = DefaultCapacitance};
            }
            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        CapacitorElm capacitorElm = sim.Create<CapacitorElm>();

        capacitorElm.capacitance = _capacitorEntity.Capacitance;
        Connectors[0].DllConnector = capacitorElm.leadIn;
        Connectors[1].DllConnector = capacitorElm.leadOut;
    }
}
