using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class GenerateId : MonoBehaviour 
{
    // Id's for each component in Prefabs, generateIds are in following order from i = 0:
    //{Batery, Ampermeter, Lamp, Capacitor, Inductor, Node, Resistor, Switch, Voltmeter, Potentiometer, Transistor NPN, Transistor PNP, Zener Diode}
    public int[] generatedIds;
    
    void Start()
    {
        generatedIds = new int[20];
        foreach (int i in generatedIds){
            generatedIds[i] = 0;
        }
    }
}