using UnityEngine;
using System.Collections;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class Connector : MonoBehaviour
{
    public string type;
    public leader DLLConnector = null;
    public Component2 Component = null;
    public Connector[] ConnectedConnectors;
    public int countOfConnected = 0;

    public void setConnectedConnectors()
    {
        ConnectedConnectors = new Connector[20];    // zatial max 20 pripojeni 
    }

    public void setDllconnector(leader dllconnector)
    {
        this.DLLConnector = dllconnector;
    }

    public void assignComponent(Component2 component)
    {
        this.Component = component;
    }
}
