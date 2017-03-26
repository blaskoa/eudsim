using System;
using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUICircuit : MonoBehaviour
{
    public static Circuit sim = new Circuit();
    private Stack _sceneItems = new Stack();
    private int _countOfMadeConnections;
    private bool stopSignal = true;

    public void RunSimulation()
    {
        GameObject playButton = GameObject.Find("PlayButton");
        GameObject pauseButton = GameObject.Find("PauseButton");
        playButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        pauseButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        sim = new Circuit();
        // setForSimulation all objects from scene 

        GetObjectsFromScene(sim);

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

    void GetObjectsFromScene(Circuit sim)  // tato funkcia len vypise vsetky objekty zo scenky, ktorych tag sa rovna toolboxitem,, je to dobry zaciatok pre vypis objektov s ktorymi si pracuje, nejak sa otaguju a je to
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag.Equals("ActiveItem"))
            {
                obj.GetComponent<GUICircuitComponent>().SetSimulationProp(sim);
                _sceneItems.Push(obj.GetComponent<GUICircuitComponent>());
            }
        }
    }

    void SimulationFlow() // tato funkcia zavola objekty so scenky, prepoji ich umelo, zavola algoritmus na zrusenie uzlov, prepoji zoznamy dllconectorov a spusti simulaciu
    {
        GUICircuitComponent[] listOfComponents = new GUICircuitComponent[_sceneItems.Count];

        GraphAlgorithm algorithm = new GraphAlgorithm();
        _sceneItems.CopyTo(listOfComponents, 0);
        ConnectionsOfComponent[] dllconnectionsOfComponents = algorithm.Untangle(listOfComponents);

        //Debug.Log(dllconnectionsOfComponents.Length);
        _countOfMadeConnections = 0;

        for (int i = 0; i < dllconnectionsOfComponents.Length; i++)
        {
            for (int a = 0; a < dllconnectionsOfComponents[i].dllconnections.Length; a++)
            {
                for (int b = 0; b < dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors.Length; b++)
                {
                    if (dllconnectionsOfComponents[i].dllconnections[a].dllconector != dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b])
                    {
                        //Debug.Log("Komponent cislo: " + i + " konektor ku ktoremu sa pripaja: " + a + " pripajany konektor: " + b + " cislo conections: " + _countOfMadeConnections);
                        sim.Connect(dllconnectionsOfComponents[i].dllconnections[a].dllconector, dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b]);
                        _countOfMadeConnections += 1;
                    }
                }
            }
        }

        Debug.Log("Simulation complete with " + _countOfMadeConnections + " connections");
        Debug.Log("Sim Elements count " + sim.elements.Count);


        _sceneItems.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if ((stopSignal == false) && (_countOfMadeConnections != 0))
        {
            try
            {
                sim.doTick();
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
                        string resErrorMsg = FindObjectOfType<Localization>().ResourceReader.GetResource("CircuitErrorMSG2");
                        FindObjectOfType<Whisp>().Say(resErrorMsg);
                        stopSignal = true;
                    }
            }
        }

    }
}
