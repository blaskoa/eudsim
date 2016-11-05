using UnityEngine;
using System.Collections;
using Assets.Skripts.Interfaces;
using ClassLibrarySharpCircuit;
using leader = ClassLibrarySharpCircuit.Circuit.Lead;

public class GUICapacitorElm : MonoBehaviour, ComponentInterface
{
    public leader[] connectors = new leader[2];
    CapacitorElm myComponent = GUICircuit.sim.Create<CapacitorElm>();

    public GUICapacitorElm()
    {
        connectors[0] = myComponent.leadIn;
        connectors[1] = myComponent.leadOut;
    }

    // Use this for initialization
    void Start()
    {
        connectors[0] = myComponent.leadIn;
        connectors[1] = myComponent.leadOut;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
