﻿using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUINode : Component2
{

    // Use this for initialization
    public void Start()
    {
        if (this.CompareTag("ActiveNode"))
        {
            Debug.Log("insertol som activeItem");
            connectors[0] = this.transform.FindChild("NodeConnector").GetComponent<Connector>();
            connectors[0].setConnectedConnectors();
        }
    }
}
