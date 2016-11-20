using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;
using UnityEngine;

public class Dllconnections
{
    public leader dllconector;
    public leader[] connectedDllconnectors;
}

public class ConnectionsOfComponent
{
    public Dllconnections[] dllconnections;
}

public class GraphAlgorithm
{
    private Stack connectorsFinished = new Stack();
    private Stack connectorsOpened = new Stack();

    private Stack dllconnectorsStack = new Stack();

    private Component searched;

    void explore(Connector actual)
    {
        if (actual != null)
        {
            connectorsOpened.Push(actual);  // put this connector to opened ones

            //Debug.Log("pripojene konectory " + actual.ConnectedConnectors.Length + " konector je: " + actual);
            //Debug.Log("rodic aktualneho komponenta: " + actual.transform.parent);

            if ((actual.DLLConnector != null) && (actual.Component != searched))   // if it is Component connector
            {
                //Debug.Log("Tento konector ma v sebe dllconector a meno jeho komponentu je: " + actual.Component.name + "velkost stacku je: " + dllconnectorsStack.Count);
                if ((!dllconnectorsStack.Contains(actual.DLLConnector)))  // if that DLLConnector is already in stack
                {
                    //Debug.Log("Prida sa do stacku pripojenych");
                    dllconnectorsStack.Push(actual.DLLConnector);
                }
            }

            else
            {
                for (int i = 0; i < actual.ConnectedConnectors.Length; i++)   // if it is not Component connector, go through all his connectors
                {
                    if ((!connectorsOpened.Contains(actual.ConnectedConnectors[i])) && (!connectorsFinished.Contains(actual.ConnectedConnectors[i])))   // if this isnt opened nor finished connector
                    {
                        explore(actual.ConnectedConnectors[i]);       // then explore it
                    }
                }
            }

            connectorsOpened.Pop();     // close this connector and put it to finished
            connectorsFinished.Push(actual);
        }
    }

    public ConnectionsOfComponent[] untangle(Component2[] components)
    {
        ConnectionsOfComponent[] connectionsOfComponent = new ConnectionsOfComponent[components.Length];

        for (int a = 0; a < components.Length; a++)
        {
            connectionsOfComponent[a] = new ConnectionsOfComponent();
            connectionsOfComponent[a].dllconnections = new Dllconnections[components[a].Connectors.Length];
            //Debug.Log("Meno prehladavaneho komponenta v untangle: " + components[a].name + " pocet konektorov: " + components[a].connectors.Length);
            searched = components[a];

            for (int b = 0; b < components[a].Connectors.Length; b++)   // for every conector of a Component
            {
                connectionsOfComponent[a].dllconnections[b] = new Dllconnections();
                connectionsOfComponent[a].dllconnections[b].dllconector = components[a].Connectors[b].DLLConnector;     // add his dllconector
                //Debug.Log("prehladavany konektor: " + b + " :" + components[a].connectors[b]);


                explore(components[a].Connectors[b]);    // find all connected dllconectors
                connectionsOfComponent[a].dllconnections[b].connectedDllconnectors = new leader[dllconnectorsStack.Count];
                dllconnectorsStack.CopyTo(connectionsOfComponent[a].dllconnections[b].connectedDllconnectors, 0);      // copy them to the list

                dllconnectorsStack.Clear();     // clear all stacks
                connectorsFinished.Clear();
                connectorsOpened.Clear();
            }
        }

        return connectionsOfComponent;
    }
}

