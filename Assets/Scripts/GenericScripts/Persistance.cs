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
    [SerializeField]
    private GameObject _ampermeterPrefab;
    [SerializeField]
    private GameObject _analogSwitchPrefab;
    [SerializeField]
    private GameObject _capacitorPrefab;
    [SerializeField]
    private GameObject _inductorPrefab;
    [SerializeField]
    private GameObject _lampPrefab;
    [SerializeField]
    private GameObject _nodePrefab;
    [SerializeField]
    private GameObject _voltmeterPrefab;

    public void Save()
    {
        GUICircuitComponent[] components = FindObjectsOfType<GUICircuitComponent>();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/test", FileMode.OpenOrCreate);
        List<SimulationElement> elementsToSerialize = new List<SimulationElement>();

        foreach (GUICircuitComponent component in components)
        {
            if (!component.CompareTag("ActiveItem"))
            {
                continue;
            }
            SimulationElement element = component.Entity;
            if (element != null)
            {
                elementsToSerialize.Add(element);
            }
        }

        binaryFormatter.Serialize(fileStream, elementsToSerialize);
        fileStream.Close();
    }

    public void Load()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/test", FileMode.Open);

        object o = binaryFormatter.Deserialize(fileStream);
        List<SimulationElement> elementToInstantiate = (List<SimulationElement>) o;

        foreach (SimulationElement simulationElement in elementToInstantiate)
        {
            Type entityType = simulationElement.GetType();
            if (entityType == typeof(BatteryEntity))
            {
                BatteryEntity concreteEntity = simulationElement as BatteryEntity;
                GameObject concreteGameObject = InstatiateGameObject(_batteryPrefab);
                concreteGameObject.GetComponent<GUIBattery>().Start();
                concreteGameObject.GetComponent<GUIBattery>().Entity = concreteEntity;
            }
            else if (entityType == typeof(ResistorEntity))
            {
                ResistorEntity concreteEntity = simulationElement as ResistorEntity;
                GameObject concreteGameObject = InstatiateGameObject(_resistorPrefab);
                concreteGameObject.GetComponent<GUIResistor>().Start();
                concreteGameObject.GetComponent<GUIResistor>().Entity = concreteEntity;
            }
            else if (entityType == typeof(VoltmeterEntity))
            {
                VoltmeterEntity concreteEntity = simulationElement as VoltmeterEntity;
                GameObject concreteGameObject = InstatiateGameObject(_voltmeterPrefab);
                concreteGameObject.GetComponent<GUIVoltmeter>().Start();
                concreteGameObject.GetComponent<GUIVoltmeter>().Entity = concreteEntity;
            }
            else if (entityType == typeof(AmpermeterEntity))
            {
                AmpermeterEntity concreteEntity = simulationElement as AmpermeterEntity;
                GameObject concreteGameObject = InstatiateGameObject(_ampermeterPrefab);
                concreteGameObject.GetComponent<GUIAmpermeter>().Start();
                concreteGameObject.GetComponent<GUIAmpermeter>().Entity = concreteEntity;
            }
            else if (entityType == typeof(AnalogSwitchEntity))
            {
                AnalogSwitchEntity concreteEntity = simulationElement as AnalogSwitchEntity;
                GameObject concreteGameObject = InstatiateGameObject(_analogSwitchPrefab);
                concreteGameObject.GetComponent<GUIAnalogSwitch>().Start();
                concreteGameObject.GetComponent<GUIAnalogSwitch>().Entity = concreteEntity;
            }
            else if (entityType == typeof(CapacitorEntity))
            {
                CapacitorEntity concreteEntity = simulationElement as CapacitorEntity;
                GameObject concreteGameObject = InstatiateGameObject(_capacitorPrefab);
                concreteGameObject.GetComponent<GUICapacitor>().Start();
                concreteGameObject.GetComponent<GUICapacitor>().Entity = concreteEntity;
            }
            else if (entityType == typeof(InductorEntity))
            {
                InductorEntity concreteEntity = simulationElement as InductorEntity;
                GameObject concreteGameObject = InstatiateGameObject(_inductorPrefab);
                concreteGameObject.GetComponent<GUIInductor>().Start();
                concreteGameObject.GetComponent<GUIInductor>().Entity = concreteEntity;
            }
            else if (entityType == typeof(LampEntity))
            {
                LampEntity concreteEntity = simulationElement as LampEntity;
                GameObject concreteGameObject = InstatiateGameObject(_lampPrefab);
                concreteGameObject.GetComponent<GUILamp>().Start();
                concreteGameObject.GetComponent<GUILamp>().Entity = concreteEntity;
            }
            else if (entityType == typeof(NodeEntity))
            {
                NodeEntity concreteEntity = simulationElement as NodeEntity;
                GameObject concreteGameObject = InstatiateGameObject(_nodePrefab);
                concreteGameObject.GetComponent<GUINode>().Start();
                concreteGameObject.GetComponent<GUINode>().Entity = concreteEntity;
            }
        }
        fileStream.Close();
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
