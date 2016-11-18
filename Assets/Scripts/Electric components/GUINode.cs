using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUINode : Component2
{
    public Resistor MyComponent;

    // Use this for initialization
    public void Start()
    {
        if (this.CompareTag("ActiveItem"))
        {
            //connectors = GetComponentsInChildren<Connector>();
            connectors[0] = gameObject.AddComponent<Connector>();
            connectors[0].setConnectedConnectors();
            connectors[0].assignComponent(this);
        }
    }
}
