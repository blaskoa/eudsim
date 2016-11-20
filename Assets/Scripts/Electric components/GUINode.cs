using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUINode : GUICircuitComponent
{

    // Use this for initialization
    public void Start()
    {
        if (this.CompareTag("ActiveNode"))
        {
            Debug.Log("insertol som activeItem");
            Connectors[0] = this.transform.FindChild("NodeConnector").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
        }
    }
}
