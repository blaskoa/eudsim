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
            Stack connectorsFinished = new Stack();     
            Stack connectorsOpened = new Stack();

            Stack dllconnectorsStack = new Stack();

            Component searched;

            void explore(Connector actual)
            {
              if (actual != null)
              {
                connectorsOpened.Push(actual);  // put this connector to opened ones

                //Debug.Log("pripojene konectory " + actual.connectedConnectors.Length + " konector je: " + actual);
                //Debug.Log("rodic aktualneho komponenta: " + actual.transform.parent);

                if ((actual.dllconnector != null) && (actual.component != searched))   // if it is component connector
                {
                    Debug.Log("Tento konector ma v sebe dllconector a meno jeho komponentu je: " + actual.component.name + "velkost stacku je: " + dllconnectorsStack.Count);
                    if ((!dllconnectorsStack.Contains(actual.dllconnector)))  // if that dllconnector is already in stack
                    {
                        Debug.Log("Prida sa do stacku pripojenych");
                        dllconnectorsStack.Push(actual.dllconnector);
                    }
                }

                else
                {
                    for (int i = 0; i < actual.connectedConnectors.Length; i++)   // if it is not component connector, go through all his connectors
                    {
                      if ((!connectorsOpened.Contains(actual.connectedConnectors[i])) && (!connectorsFinished.Contains(actual.connectedConnectors[i])))   // if this isnt opened nor finished connector
                      {
                             explore(actual.connectedConnectors[i]);       // then explore it
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
                    connectionsOfComponent[a].dllconnections = new Dllconnections[components[a].connectors.Length];
                    Debug.Log("Meno prehladavaneho komponenta v untangle: " + components[a].name + " pocet konektorov: " + components[a].connectors.Length);
                    searched = components[a];

                 for (int b = 0; b < components[a].connectors.Length; b++)   // for every conector of a component
                    {
                         connectionsOfComponent[a].dllconnections[b] = new Dllconnections();
                         connectionsOfComponent[a].dllconnections[b].dllconector = components[a].connectors[b].dllconnector;     // add his dllconector
                         Debug.Log("prehladavany konektor: " + b + " :" + components[a].connectors[b]);
                                 

                         explore(components[a].connectors[b]);    // find all connected dllconectors
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

