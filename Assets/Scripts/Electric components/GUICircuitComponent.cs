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

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public virtual void CopyValues(GUICircuitComponent old)
    {
        
    }
}

