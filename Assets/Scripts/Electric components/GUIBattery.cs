using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;


public class GUIBattery : GUICircuitComponent
{
    public Circuit.Lead[] DllConnectors;
    public VoltageInput MyComponent;
    public Ground MyComponentGround;

    private BatteryEntity _batteryEntity;

    public double MaxVoltage
    {
        get { return _batteryEntity.MaxVoltage; }
        set
        {
            MyComponent.maxVoltage = value;
            _batteryEntity.MaxVoltage = value;
        }   // GUI check - accept only positive integer
    }

    public void SetMaxVoltage(double val)
    {
        MaxVoltage = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddNumeric("MaxVoltagePropertyLabel", MaxVoltage.ToString(), MaxVoltage.GetType().ToString(), SetMaxVoltage, false);
    }

    // Use this for initialization
    public void Start()     // public for testing purposes
    {
        if (this.CompareTag("ActiveItem"))
        {
            if (_batteryEntity == null)
            {
                _batteryEntity = new BatteryEntity();
            }
            SetSimulationProp(GUICircuit.sim);
            Connectors[0] = transform.FindChild("PlusConnector").GetComponent<Connector>();
            Connectors[1] = transform.FindChild("MinusConnector").GetComponent<Connector>();
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
        MyComponent = sim.Create<VoltageInput>(Voltage.WaveType.DC);
        MyComponentGround = sim.Create<Ground>();
        DllConnectors[0] = MyComponent.leadPos;
        DllConnectors[1] = MyComponentGround.leadIn;
    }

    public override void SetDllConnectors()
    {
        Connectors[0].SetDllConnector(DllConnectors[0]);
        Connectors[1].SetDllConnector(DllConnectors[1]);
    }

    public override SimulationElement GetEntity()
    {
        _batteryEntity.PositionX = transform.position.x;
        _batteryEntity.PositionY = transform.position.y;
        return _batteryEntity;
    }

    public void SetEntity(BatteryEntity entity)
    {
        _batteryEntity = entity;
        transform.position = new Vector3(entity.PositionX, entity.PositionY);
        MaxVoltage = entity.MaxVoltage;
    }
}