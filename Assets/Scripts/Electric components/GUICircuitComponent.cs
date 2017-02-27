using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;
using UnityEngine;

public abstract class GUICircuitComponent : MonoBehaviour
{
    public Connector[] Connectors = new Connector[2];

    public virtual void GetProperties()
    {

    }

    public virtual void SetProperties()
    {

    }

    public virtual void SetSimulationProp(Circuit sim)
    {

    }

    public virtual void SetDllConnectors()
    {

    }

    public virtual SimulationElement GetEntity()
    {
        return null;
    }
}

