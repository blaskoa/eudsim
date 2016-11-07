using UnityEngine;
using System.Collections;
using ClassLibrarySharpCircuit;

public class GUICircuit : MonoBehaviour
{
    public static Circuit sim = new Circuit();

    // Use this for initialization
    void Start()
    {
        //simulacia obvodu

        GUIBattery battery = gameObject.AddComponent<GUIBattery>();
        battery.inicializeGUIBattery();

        GUIResistor res0 = gameObject.AddComponent<GUIResistor>();
        res0.inicializeGUIResistor();

        GUIResistor res1 = gameObject.AddComponent<GUIResistor>();
        res1.inicializeGUIResistor();

        sim.Connect(battery.connectors[0], res0.connectors[0]);
        sim.Connect(res0.connectors[1], res1.connectors[0]);
        sim.Connect(res1.connectors[1], battery.connectors[1]);

        battery.voltageDelta = 25;

        for (int x = 1; x <= 10; x++)
        {
            sim.doTick();
            // Ohm's Law
            Debug.Log("res0 voltage " + res0.voltageDelta + "volt0 voltage" + battery.voltageDelta); // V = I x R
        }

        Debug.Log("Simulation complete");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
