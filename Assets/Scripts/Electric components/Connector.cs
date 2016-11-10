using UnityEngine;
using System.Collections;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class Connector : MonoBehaviour
{
    public leader DLLConnector = null;
    public Component2 Component = null;
    public Connector[] ConnectedConnectors;

    public void initialize()
    {
        ConnectedConnectors = new Connector[20];    // zatial max 20 pripojeni 
    }

    public void assignDllconnector(leader dllconnector)
    {
        this.DLLConnector = dllconnector;
    }

    public void assignComponent(Component2 component)
    {
        this.Component = component;
    }
}
