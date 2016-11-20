using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class Connector : MonoBehaviour
{
    public string Type;
    public Circuit.Lead DllConnector = null;
    public GUICircuitComponent Component = null;
    public Connector[] ConnectedConnectors;
    public int CountOfConnected = 0;

    public void SetConnectedConnectors()
    {
        ConnectedConnectors = new Connector[20];    // zatial max 20 pripojeni 
    }

    public void SetDllConnector(Circuit.Lead dllConnector)
    {
        this.DllConnector = dllConnector;
    }

    public void AssignComponent(GUICircuitComponent component)
    {
        this.Component = component;
    }
}
