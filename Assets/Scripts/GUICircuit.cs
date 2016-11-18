using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUICircuit : MonoBehaviour
{
    public static Circuit sim = new Circuit();
    private Stack sceneItems = new Stack();
    private int countOfMadeConnections = 0;

    // Use this for initialization
    public void Sim0()
    {
        //simulacia obvodu
        Debug.Log("Simulation complete");
    }

    // funkcia bez popisu, nepouzita
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
                sceneItems.Push(obj.GetComponent<Component2>());
            }
        }
    }

    void SimulationFlow() // tato funkcia zavola objekty so scenky, prepoji ich umelo, zavola algoritmus na zrusenie uzlov, prepoji zoznamy dllconectorov a spusti simulaciu
    {
        Component2[] listOfComponents = new Component2[4];

        GraphAlgorithm algorithm = new GraphAlgorithm();
        sceneItems.CopyTo(listOfComponents, 0);
        /*   ConnectionsOfComponent[] dllconnectionsOfComponents = algorithm.untangle(listOfComponents);

           Debug.Log(dllconnectionsOfComponents.Length);

           //sim.Connect(dllconnectionsOfComponents[0].dllconnections[0].connectedDllconnectors[0], dllconnectionsOfComponents[0].dllconnections[0].dllconector);

           for (int i = 0; i < dllconnectionsOfComponents.Length; i++)
           {
               for (int a = 0; a < dllconnectionsOfComponents[i].dllconnections.Length; a++)
               {
                   for (int b = 0; b < dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors.Length; b++)
                   {
                       if (dllconnectionsOfComponents[i].dllconnections[a].dllconector != dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b])
                       {
                           Debug.Log(i + a + b);
                           //sim.Connect(dllconnectionsOfComponents[i].dllconnections[a].dllconector, dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b]);
                           countOfMadeConnections += 1;
                       }
                   }
               }
           }
           */
        Debug.Log("Simulation Begin with" + countOfMadeConnections + "connections");
        Debug.Log("Sim Elements count " + sim.elements.Count);


        for (int x = 1; x <= 1; x++)
        {
            sim.doTick();
            Debug.Log(sceneItems.Count + "komponentov");
            for (int i = 0; i < sceneItems.Count; i++)
            {
                if (listOfComponents[i].GetType() == typeof(GUIBattery))
                    Debug.Log("Ja SOM BATERKA");
                if (listOfComponents[i].GetType() == typeof(GUIResistor))
                    Debug.Log("Ja SOM RESISTOR");
            }

            //  TENTO KOD JE DO BUDUCNA POTREBNY, AVSAK PRE JEDNODUCHSIE DEBUGOVANIE HO ZANEDBAME A ZATIAL VYTVARAME CONNECTIONS PRIAMO PRI CIARACH
            //  Debug.Log("battery voltage " + battery.MyComponent.getVoltageDelta() + "lamp voltage" + lamp.MyComponent.getVoltageDelta()); // V = I x R
        }

        Debug.Log("Simulation complete with" + countOfMadeConnections + "connections");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
