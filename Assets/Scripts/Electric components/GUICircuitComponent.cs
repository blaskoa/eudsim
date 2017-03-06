using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;
using UnityEngine;

public abstract class GUICircuitComponent : MonoBehaviour
{
    public List<Connector> Connectors;
    public abstract SimulationElement Entity { get; set; }

    public abstract void GetProperties();

    public abstract void SetSimulationProp(Circuit sim);

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
    }

    protected void SetTransformForEntity(SimulationElement simulationElement)
    {
        simulationElement.PositionX = transform.position.x;
        simulationElement.PositionY = transform.position.y;
        simulationElement.RotationZ = transform.rotation.z;
        simulationElement.RotationW = transform.rotation.w;
    }
}

