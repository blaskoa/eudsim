using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUIAnalogSwitch : MonoBehaviour, ComponentInterface
{
    public leader[] connectors = new leader[2];
    AnalogSwitch myComponent = GUICircuit.sim.Create<AnalogSwitch>();

    public GUIAnalogSwitch()
    {
        connectors[0] = myComponent.leadIn;
        connectors[1] = myComponent.leadOut;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
