using UnityEngine;
using System.Collections;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class Connector : MonoBehaviour
{
    public leader dllconnector = null;
    public Component2 component = null;
    public Connector[] connectedConnectors;  
    
    public void initialize()
    {
        connectedConnectors = new Connector[20];    // zatial max 20 pripojeni 
    }   

    public void assignDllconnector(leader dllconnector)
    {
        this.dllconnector = dllconnector;
    }

    public void assignComponent(Component2 component)
    {
        this.component = component;
    }
   
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}
