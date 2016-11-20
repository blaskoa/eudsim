using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using ClassLibrarySharpCircuit;

public class Dllconnections
{
    public Circuit.Lead dllconector;
    public Circuit.Lead[] connectedDllconnectors;
}

public class ConnectionsOfComponent
{
    public Dllconnections[] dllconnections;
}

public class GraphAlgorithm
{
    private Stack _connectorsFinished = new Stack();
    private Stack _connectorsOpened = new Stack();

    private Stack _dllconnectorsStack = new Stack();

    private Component _searched;

    public void Explore(Connector actual)
    {
        if (actual != null)
        {
            _connectorsOpened.Push(actual);  // put this connector to opened ones

            //Debug.Log("pripojene konectory " + actual.ConnectedConnectors.Length + " konector je: " + actual);
            //Debug.Log("rodic aktualneho komponenta: " + actual.transform.parent);

            if ((actual.DllConnector != null) && (actual.Component != _searched))   // if it is Component connector
            {
                //Debug.Log("Tento konector ma v sebe dllconector a meno jeho komponentu je: " + actual.Component.name + "velkost stacku je: " + _dllconnectorsStack.Count);
                if ((!_dllconnectorsStack.Contains(actual.DllConnector)))  // if that DllConnector is already in stack
                {
                    //Debug.Log("Prida sa do stacku pripojenych");
                    _dllconnectorsStack.Push(actual.DllConnector);
                }
            }

            else
            {
                for (int i = 0; i < actual.ConnectedConnectors.Length; i++)   // if it is not Component connector, go through all his connectors
                {
                    if ((!_connectorsOpened.Contains(actual.ConnectedConnectors[i])) && (!_connectorsFinished.Contains(actual.ConnectedConnectors[i])))   // if this isnt opened nor finished connector
                    {
                        Explore(actual.ConnectedConnectors[i]);       // then Explore it
                    }
                }
            }

            _connectorsOpened.Pop();     // close this connector and put it to finished
            _connectorsFinished.Push(actual);
        }
    }

    public ConnectionsOfComponent[] Untangle(GUICircuitComponent[] components)
    {
        ConnectionsOfComponent[] connectionsOfComponent = new ConnectionsOfComponent[components.Length];

        for (int a = 0; a < components.Length; a++)
        {
            connectionsOfComponent[a] = new ConnectionsOfComponent();
            connectionsOfComponent[a].dllconnections = new Dllconnections[components[a].Connectors.Length];
            //Debug.Log("Meno prehladavaneho komponenta v Untangle: " + components[a].name + " pocet konektorov: " + components[a].connectors.Length);
            _searched = components[a];

            for (int b = 0; b < components[a].Connectors.Length; b++)   // for every conector of a Component
            {
                connectionsOfComponent[a].dllconnections[b] = new Dllconnections();
                connectionsOfComponent[a].dllconnections[b].dllconector = components[a].Connectors[b].DllConnector;     // add his dllconector
                //Debug.Log("prehladavany konektor: " + b + " :" + components[a].connectors[b]);


                Explore(components[a].Connectors[b]);    // find all connected dllconectors
                connectionsOfComponent[a].dllconnections[b].connectedDllconnectors = new Circuit.Lead[_dllconnectorsStack.Count];
                _dllconnectorsStack.CopyTo(connectionsOfComponent[a].dllconnections[b].connectedDllconnectors, 0);      // copy them to the list

                _dllconnectorsStack.Clear();     // clear all stacks
                _connectorsFinished.Clear();
                _connectorsOpened.Clear();
            }
        }

        return connectionsOfComponent;
    }
}

