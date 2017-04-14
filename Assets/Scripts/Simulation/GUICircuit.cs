using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Localization;
using ClassLibrarySharpCircuit;
using System.Collections.Generic;

public class GUICircuit : MonoBehaviour
{
    public static List<Circuit> simList = new List<Circuit>();
    //private Stack _sceneItems = new Stack();
    private int _countOfMadeConnections;
    private bool stopSignal = true;
    private List<GUICircuitComponent[]> _listOfNetworks = new List<GUICircuitComponent[]>();

    public void RunSimulation()
    {
        GameObject playButton = GameObject.Find("PlayButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        playButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = true;

        // setForSimulation all objects from scene 

        GetObjectsFromScene();
        stopSignal = false;
        SimulationFlow();
    }

    public void StopSimulation()
    {
        GameObject playButton = GameObject.Find("PlayButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        playButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        stopSignal = true;
    }

    List<GUICircuitComponent> traveledNodes = new List<GUICircuitComponent>();
    void DFS(GUICircuitComponent startNode)
    {
        if (!traveledNodes.Contains(startNode))
        {
            traveledNodes.Add(startNode);
            foreach (Connector c in startNode.Connectors[0].ConnectedConnectors)
            {
                if ((c != startNode.Connectors[0])|| (c != startNode.Connectors[1])) {
                    DFS(c.Component);
                }
            }

            foreach (Connector c in startNode.Connectors[1].ConnectedConnectors)
            {
                if ((c != startNode.Connectors[0]) || (c != startNode.Connectors[1]))
                {
                    DFS(c.Component);
                }
            }
        }
    }

    void GetObjectsFromScene()
    {
        List<GUICircuitComponent> sceneItems = new List<GUICircuitComponent>();
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag.Equals("ActiveItem"))
            {
                //obj.GetComponent<GUICircuitComponent>().SetSimulationProp(sim);
                //_sceneItems.Push(obj.GetComponent<GUICircuitComponent>());
                sceneItems.Add(obj.GetComponent<GUICircuitComponent>());
            }
        }

        while (sceneItems.Count > 0)
        {
            DFS(sceneItems[0]);
            GUICircuitComponent[] newList = new GUICircuitComponent[traveledNodes.Count];

            traveledNodes.CopyTo(newList);
            _listOfNetworks.Add(newList);

            for (int i = 0; i < sceneItems.Count; i++)
            {
                if (traveledNodes.Contains(sceneItems[i]))
                {
                    sceneItems.RemoveAt(i);
                    i--;
                }
            }
            traveledNodes.Clear();
        }
    }

    void SimulationFlow() // tato funkcia zavola objekty so scenky, prepoji ich umelo, zavola algoritmus na zrusenie uzlov, prepoji zoznamy dllconectorov a spusti simulaciu
    {
        foreach (GUICircuitComponent[] network in _listOfNetworks) 
        {
            int numOfBatteries = 0;
            foreach (GUICircuitComponent component in network)
            {
                if (component.GetType() == GameObject.Find("Accumulator").GetComponent<GUICircuitComponent>().GetType())
                {
                    numOfBatteries++;
                }
            }

            if ((numOfBatteries < 2)&&(network.Length > 1))
            {
                Circuit newSimulation = new Circuit();
                simList.Add(newSimulation);
                foreach (GUICircuitComponent component in network)
                {
                    component.SetSimulationProp(newSimulation);
                }

                GraphAlgorithm algorithm = new GraphAlgorithm();
                ConnectionsOfComponent[] dllconnectionsOfComponents = algorithm.Untangle(network);

                //Debug.Log(dllconnectionsOfComponents.Length);
                _countOfMadeConnections = 0;

                List<Circuit.Lead[]> alreadyConnected = new List<Circuit.Lead[]>();

                for (int i = 0; i < dllconnectionsOfComponents.Length; i++)
                {
                    for (int a = 0; a < dllconnectionsOfComponents[i].dllconnections.Length; a++)
                    {
                        for (int b = 0; b < dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors.Length; b++)
                        {
                            if (dllconnectionsOfComponents[i].dllconnections[a].dllconector != dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b])
                            {
                                Boolean alreadyThere = false;
                                foreach (Circuit.Lead[] pair in alreadyConnected)
                                {
                                    if ((pair[0] == dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b]) && (pair[1] == dllconnectionsOfComponents[i].dllconnections[a].dllconector))
                                    {
                                        alreadyThere = true;
                                    }
                                }

                                if (alreadyThere == false)
                                {
                                    Circuit.Lead[] newPair = new Circuit.Lead[2];
                                    newPair[0] = dllconnectionsOfComponents[i].dllconnections[a].dllconector;
                                    newPair[1] = dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b];
                                    alreadyConnected.Add(newPair);
                                    //Debug.Log("Komponent cislo: " + i + " konektor ku ktoremu sa pripaja: " + a + " pripajany konektor: " + b + " cislo conections: " + _countOfMadeConnections);
                                    newSimulation.Connect(dllconnectionsOfComponents[i].dllconnections[a].dllconector, dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b]);
                                    _countOfMadeConnections += 1;
                                }
                            }
                        }
                    }
                }

                Debug.Log("Simulation complete with " + _countOfMadeConnections + " connections");
                Debug.Log("Sim Elements count " + newSimulation.elements.Count);
            }
        }
        _listOfNetworks.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if ((stopSignal == false) && (_countOfMadeConnections != 0))
        {
            try
            {
                foreach (Circuit sim in simList)
                {
                    sim.doTick();
                }
            }
            catch (Circuit.Exception e)
            {
                MainMenuButtons.CircuitError(e.element);
                stopSignal = true;
            }
            catch (NullReferenceException e)
            {
                stopSignal = true;
            }
            //MainMenuButtons.CircuitError(e.element);
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.tag.Equals("ActiveItem") && obj.name.Contains("Ampermeter"))
                    if (obj.GetComponent<GUIAmpermeter>().ResistorComponent.getCurrent() > 10)
                    {
                        string resErrorMsg = ResourceReader.Instance.GetResource("CircuitErrorMSG2");
                        FindObjectOfType<Whisp>().Say(resErrorMsg);
                        stopSignal = true;
                    }
            }
        }

    }
}
