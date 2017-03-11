using System;
using UnityEngine;
using System.Collections.Generic;
using ClassLibrarySharpCircuit;

public class Connector : MonoBehaviour
{
    public Circuit.Lead DllConnector;
    public GUICircuitComponent Component;
    public List<Connector> ConnectedConnectors;
    public int TemporaryId { get; set; }

    public void Initialize(GUICircuitComponent component)
    {
        if (ConnectedConnectors == null)
        {
            ConnectedConnectors = new List<Connector>(20);
        }

        Component = component;
    }
}
