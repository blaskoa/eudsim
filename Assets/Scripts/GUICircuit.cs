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
        /*
        GUIBattery battery = gameObject.AddComponent<GUIBattery>();
        GUIResistor res0 = gameObject.AddComponent<GUIResistor>();
        GUIResistor res1 = gameObject.AddComponent<GUIResistor>();
               
        sim.Connect(battery.connectors[0], res0.connectors[0]);
        sim.Connect(res0.connectors[1], res1.connectors[0]);
        sim.Connect(res1.connectors[1], battery.connectors[1]);

        for (int x = 1; x <= 10; x++)
        {
            sim.doTick();
            // Ohm's Law
            Debug.Log("res0 voltage " + res0.getVoltageDelta() + "volt0 voltage" + battery.getVoltageDelta()); // V = I x R
        }

        Debug.Log("Simulation complete");
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
