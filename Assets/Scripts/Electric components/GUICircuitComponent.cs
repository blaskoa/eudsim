using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;
using UnityEngine;

public abstract class GUICircuitComponent : MonoBehaviour
{
    public int id;
    public static UndoList globalUndoList = new UndoList();
    public static int idCounter = 0;

    public List<Connector> Connectors;
    public abstract SimulationElement Entity { get; set; }
    private string _name = "Node";
    
    public void SetName(string val)
    {
        _name = val;
    }

    public abstract void GetProperties();
    public abstract string GetPropertiesForExport();
    public abstract void SetSimulationProp(Circuit sim);

    public virtual void Awake()
    {

    }

    protected void SetAndInitializeConnectors()
    {
        Connectors = GetComponentsInChildren<Connector>().ToList();
        foreach (Connector connector in Connectors)
        {
            connector.Initialize(this);
        }
    }

    protected void TransformFromEntity(SimulationElement simulationElement)
    {
        transform.position = new Vector3(simulationElement.PositionX, simulationElement.PositionY);
        transform.rotation = new Quaternion(0, 0, simulationElement.RotationZ, simulationElement.RotationW);

        int indexer = 0;
        foreach (Connector connector in Connectors)
        {
            connector.TemporaryId = simulationElement.ConnectorIds[indexer];
            indexer++;
        }
    }

    protected void FillEntity(SimulationElement simulationElement)
    {
        simulationElement.PositionX = transform.position.x;
        simulationElement.PositionY = transform.position.y;
        simulationElement.RotationZ = transform.rotation.z;
        simulationElement.RotationW = transform.rotation.w;
        simulationElement.Id = GetInstanceID();
        
        List<int> connectorIdList = new List<int>(Connectors.Count);
        foreach (Connector connector in Connectors)
        {
            connectorIdList.Add(connector.GetInstanceID());
        }

        simulationElement.ConnectorIds = connectorIdList;
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public virtual void CopyValues(GUICircuitComponent old)
    {

    }

    public virtual void SetAllProperties(List<float> properties)
    {
    }

    public virtual void SetId(int id)
    {

    }

    public virtual int GetId()
    {
        return -1;
    }
}

