using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Entities;

public class Persistance : MonoBehaviour
{
    [SerializeField]
    private GameObject _batteryPrefab;
    [SerializeField]
    private GameObject _resistorPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        GUICircuitComponent[] components = FindObjectsOfType<GUICircuitComponent>();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/test", FileMode.OpenOrCreate);
        List<SimulationElement> elementsToSerialize = new List<SimulationElement>();
        foreach (GUICircuitComponent component in components)
        {
            if (component.tag != "ActiveItem")
            {
                continue;
            }
            SimulationElement element = component.GetEntity();
            if (element != null)
            {
                elementsToSerialize.Add(component.GetEntity());
            }
        }

        bf.Serialize(fs, elementsToSerialize);
        fs.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs2 = new FileStream(Application.persistentDataPath + "/test", FileMode.Open);

        object o = bf.Deserialize(fs2);
        List<SimulationElement> elementToInstantiate = (List<SimulationElement>) o;

        foreach (SimulationElement simulationElement in elementToInstantiate)
        {
            Type entityType = simulationElement.GetType();
            if (entityType == typeof(BatteryEntity))
            {
                BatteryEntity batteryEntity = simulationElement as BatteryEntity;
                GameObject battery = InstatiateGameObject(_batteryPrefab);
                battery.GetComponent<GUIBattery>().Start();
                battery.GetComponent<GUIBattery>().SetEntity(batteryEntity);
            }
            else if (entityType == typeof(ResistorEntity))
            {
                ResistorEntity resistorEntity = simulationElement as ResistorEntity;
                GameObject resistor = InstatiateGameObject(_resistorPrefab);
                resistor.GetComponent<GUIResistor>().Start();
                resistor.GetComponent<GUIResistor>().SetEntity(resistorEntity);
            }
        }
        fs2.Close();
    }

    private GameObject InstatiateGameObject(GameObject gameObject)
    {
        GameObject activeGameObject = Instantiate(gameObject);
        activeGameObject.tag = "ActiveItem";
        activeGameObject.layer = 8; //Name of 8th layer is ActiveItem
        activeGameObject.transform.localScale = new Vector3(1, 1, 0);
        activeGameObject.GetComponent<SpriteRenderer>().enabled = true;
        activeGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
        for (int i = 0; i < activeGameObject.transform.childCount; i++)
        {
            activeGameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ActiveItem";
            activeGameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            activeGameObject.transform.GetChild(i).gameObject.layer = 8;
        }
        return activeGameObject;
    }
}
