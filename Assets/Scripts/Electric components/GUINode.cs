using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUINode : GUICircuitComponent
{
    public void Awake()
    {
        if (this.CompareTag("ActiveNode"))
        {
            Debug.Log("insertol som node");
            Connectors[0] = this.transform.FindChild("NodeConnector").GetComponent<Connector>();
            Connectors[0].SetConnectedConnectors();
        }
    }

    // Use this for initialization
    public void Start()
    {

    }
}
