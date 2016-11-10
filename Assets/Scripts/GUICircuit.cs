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

        sim.Connect(battery.dllconnectors[0], res0.dllconnectors[0]);
        sim.Connect(res0.dllconnectors[1], res1.dllconnectors[0]);
        sim.Connect(res1.dllconnectors[1], battery.dllconnectors[1]);

        for (int x = 1; x <= 10; x++)
        {
            sim.doTick();

            Debug.Log("battery voltage " + battery.myComponent.maxVoltage + "res voltage" + res0.myComponent.getVoltageDelta()); // V = I x R
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

        uzol.initialize();
        Debug.Log(battery.connectors[0].connectedConnectors.Length);

        battery.connectors[0].connectedConnectors[0] = uzol;         //prvy konector baterky ma neinicializovany list konectorov ku ktorym sa pripaja
        lamp.connectors[0].connectedConnectors[0] = uzol;

        uzol.connectedConnectors[0] = battery.connectors[0];
        uzol.connectedConnectors[1] = lamp.connectors[1];

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

            Debug.Log("battery voltage " + battery.myComponent.getVoltageDelta() + "lamp voltage" + lamp.myComponent.getVoltageDelta()); // V = I x R
        }

        Debug.Log("Simulation complete");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
