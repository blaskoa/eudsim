using UnityEngine;
using System.Collections.Generic;
using ClassLibrarySharpCircuit;

public class Connector : MonoBehaviour
{
    public Circuit.Lead DllConnector;
    public GUICircuitComponent Component;
    public List<Connector> ConnectedConnectors;

    public void Initialize(GUICircuitComponent component)
    {
        Component = component;
        ConnectedConnectors = new List<Connector>(20);
    }
}
