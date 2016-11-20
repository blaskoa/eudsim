using UnityEngine;
using System.Collections;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class Connector : MonoBehaviour
{
    public string Type;
    public leader DLLConnector = null;
    public Component2 Component = null;
    public Connector[] ConnectedConnectors;
    public int CountOfConnected = 0;

    public void SetConnectedConnectors()
    {
        ConnectedConnectors = new Connector[20];    // zatial max 20 pripojeni 
    }

    public void SetDllconnector(leader dllconnector)
    {
        this.DLLConnector = dllconnector;
    }

    public void AssignComponent(Component2 component)
    {
        this.Component = component;
    }
}
