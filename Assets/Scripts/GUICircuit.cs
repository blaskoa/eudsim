using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUICircuit : MonoBehaviour
{
    public static Circuit sim = new Circuit();

    // Use this for initialization
    public void Sim0()
    {
        //simulacia obvodu

        GUIBattery battery = gameObject.AddComponent<GUIBattery>();
        GUIResistor res0 = gameObject.AddComponent<GUIResistor>();
        GUIResistor res1 = gameObject.AddComponent<GUIResistor>();

        battery.Start();
        res0.Start();
        res1.Start();

        sim.Connect(battery.DLLConnectors[0], res0.DLLConnectors[0]);
        sim.Connect(res0.DLLConnectors[1], res1.DLLConnectors[0]);
        sim.Connect(res1.DLLConnectors[1], battery.DLLConnectors[1]);

        for (int x = 1; x <= 10; x++)
        {
            sim.doTick();

            Debug.Log("battery voltage " + battery.MyComponent.maxVoltage + "res voltage" + res0.MyComponent.getVoltageDelta()); // V = I x R
        }

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
            if (obj.tag.Equals("ToolboxItem"))
            {
                //Debug.Log(obj.name);
            }
        }
    }

    void SimulationFlow() // tato funkcia zavola objekty so scenky, prepoji ich umelo, zavola algoritmus na zrusenie uzlov, prepoji zoznamy dllconectorov a spusti simulaciu
    {
        Component2[] listOfComponents = new Component2[2];

        GUIBattery battery = (GUIBattery)UnityEngine.Object.FindObjectsOfType(typeof(GUIBattery))[0];
        GUILamp lamp = (GUILamp)UnityEngine.Object.FindObjectsOfType(typeof(GUILamp))[0];

        Connector uzol = null;
        foreach (Connector obj in (Connector[])UnityEngine.Object.FindObjectsOfType(typeof(Connector)))
        {
            if (obj.transform.parent == null)  // uzol nema rodica
            {
                uzol = obj;
            }
        }

        uzol.setConnectedConnectors();
        Debug.Log(battery.connectors[0].ConnectedConnectors.Length);

        battery.connectors[0].ConnectedConnectors[0] = uzol;         //prvy konector baterky ma neinicializovany list konectorov ku ktorym sa pripaja
        lamp.connectors[0].ConnectedConnectors[0] = uzol;

        uzol.ConnectedConnectors[0] = battery.connectors[0];
        uzol.ConnectedConnectors[1] = lamp.connectors[1];

        listOfComponents[0] = battery;
        listOfComponents[1] = lamp;

        GraphAlgorithm algorithm = new GraphAlgorithm();
        ConnectionsOfComponent[] dllconnectionsOfComponents = algorithm.untangle(listOfComponents);

        Debug.Log("Tu si sa dostal");

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
                        sim.Connect(dllconnectionsOfComponents[i].dllconnections[a].dllconector, dllconnectionsOfComponents[i].dllconnections[a].connectedDllconnectors[b]);
                    }
                }
            }
        }

        for (int x = 1; x <= 10; x++)
        {
            sim.doTick();

            Debug.Log("battery voltage " + battery.MyComponent.getVoltageDelta() + "lamp voltage" + lamp.MyComponent.getVoltageDelta()); // V = I x R
        }

        Debug.Log("Simulation complete");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
