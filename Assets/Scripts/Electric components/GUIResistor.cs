using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUIResistor : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public Resistor MyComponent;
    private ResistorEntity _resistorEntity;

    public double Resistance
    {
        get { return _resistorEntity.Resistance; }
        set
        {
            MyComponent.resistance = value;
            _resistorEntity.Resistance = value;
        }   // GUI check - accept only positive integer
    }

    public void SetResistance(double val)
    {
        Resistance = val;
    }

    public double GetVoltageDelta()
    {
        return MyComponent.getVoltageDelta();
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("ResistancePropertyLabel", Resistance.ToString(), Resistance.GetType().ToString(), SetResistance, true, -15.4f, 150.6f);
    }

    // Use this for initialization
    public void Start()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_resistorEntity == null)
            {
                _resistorEntity = new ResistorEntity();
            }

            SetSimulationProp(GUICircuit.sim);
            Connectors[0] = transform.FindChild("Connector1").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("Connector2").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
            Connectors[1].SetConnectedConnectors();
            Connectors[0].AssignComponent(this);
            Connectors[1].AssignComponent(this);
            SetDllConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");
        DllConnectors = new Circuit.Lead[2];
        MyComponent = sim.Create<Resistor>();
        DllConnectors[0] = MyComponent.leadIn;
        DllConnectors[1] = MyComponent.leadOut;
    }

    public override void SetDllConnectors()
    {
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }

    public override SimulationElement GetEntity()
    {
        _resistorEntity.PositionX = transform.position.x;
        _resistorEntity.PositionY = transform.position.y;
        return _resistorEntity;
    }

    public void SetEntity(ResistorEntity entity)
    {
        _resistorEntity = entity;
        transform.position = new Vector3(entity.PositionX, entity.PositionY);
        Resistance = entity.Resistance;
    }
}
