using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUICircuit : MonoBehaviour
{
    public static Circuit sim = new Circuit();
    private Stack _sceneItems = new Stack();
    private int _countOfMadeConnections;

    public void RunSimulation()
    {
        GetObjectsFromScene();

        SimulationFlow();
    }

    void GetObjectsFromScene()  // tato funkcia len vypise vsetky objekty zo scenky, ktorych tag sa rovna toolboxitem,, je to dobry zaciatok pre vypis objektov s ktorymi si pracuje, nejak sa otaguju a je to
    {
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag.Equals("ActiveItem"))
            {
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


        for (int x = 1; x <= 1; x++)
        {
            sim.doTick();
            Debug.Log("count of components:" + _sceneItems.Count);
            for (int i = 0; i < _sceneItems.Count; i++)
            {
                if (listOfComponents[i].GetType() == typeof(GUIBattery))
                {
                    Debug.Log(i + "Battery " + listOfComponents[i].GetComponent<GUIBattery>().GetVoltageDelta());
                }
                if (listOfComponents[i].GetType() == typeof(GUIResistor))
                {
                    Debug.Log(i + "Resistor " + listOfComponents[i].GetComponent<GUIResistor>().GetVoltageDelta());
                }
            }
        }

        _sceneItems.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
