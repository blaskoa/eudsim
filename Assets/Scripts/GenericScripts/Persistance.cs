using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
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
    [SerializeField]
    private GameObject _linePrefab;

    public void Save()
    {
        GUICircuitComponent[] components = FindObjectsOfType<GUICircuitComponent>();
        Line[] lines = FindObjectsOfType<Line>();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/project.es", FileMode.OpenOrCreate);
        List<SimulationElement> elementsToSerialize = new List<SimulationElement>();
        List<LineEntity> linesToSerialize = new List<LineEntity>();

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

        foreach (Line line in lines)
        {
            linesToSerialize.Add(new LineEntity
            {
                StartConnectorId = line.Begin.GetComponentInChildren<Connector>().GetInstanceID(),
                EndConnectorId = line.End.GetComponentInChildren<Connector>().GetInstanceID(),
                LineType = line.TypeOfLine
            });
        }

        SerializationPackage package = new SerializationPackage
        {
            LineEntities = linesToSerialize,
            SimulationElements = elementsToSerialize
        };


        binaryFormatter.Serialize(fileStream, package);
        fileStream.Close();
    }

    public void Load()
    {
        ClearScene();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.persistentDataPath + "/project.es", FileMode.Open);

        object o = binaryFormatter.Deserialize(fileStream);
        SerializationPackage package = (SerializationPackage) o;

        foreach (SimulationElement simulationElement in package.SimulationElements)
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

        List<Connector> connectors = FindObjectsOfType<Connector>().ToList();
        connectors =
            connectors.Where(
                x => x.transform.parent != null && (x.transform.parent.CompareTag("ActiveItem") || x.transform.parent.CompareTag("ActiveNode"))).ToList();

        foreach (LineEntity packageLineEntity in package.LineEntities)
        {
            Connector start = connectors.Find(x => x.TemporaryId == packageLineEntity.StartConnectorId);
            Connector end = connectors.Find(x => x.TemporaryId == packageLineEntity.EndConnectorId);

            GameObject linePrefab = Instantiate(_linePrefab);
            Line line = linePrefab.AddComponent<Line>();
            line.Begin = start.gameObject;
            line.End = end.gameObject;
            line.TypeOfLine = packageLineEntity.LineType;
            line.EndPos = end.transform.position;
            line.StartPos = start.transform.position;

            end.GetComponent<Connectable>().AddConnected(start.gameObject);
            start.GetComponent<Connectable>().AddConnected(end.gameObject);

            start.ConnectedConnectors.Add(end);
            end.ConnectedConnectors.Add(start);
        }

        fileStream.Close();
    }

    public void ClearScene()
    {
        List<GameObject> elementGameObjects = FindObjectsOfType<GUICircuitComponent>().ToList()
            .Where(x => x.CompareTag("ActiveItem"))
            .Select(x => x.gameObject).ToList();

        List<GameObject> lineGameObjects = FindObjectsOfType<Line>().ToList()
            .Select(x => x.gameObject).ToList();

        foreach (GameObject elementGameObject in elementGameObjects)
        {
            Destroy(elementGameObject);
        }
        foreach (GameObject lineGameObject in lineGameObjects)
        {
            Destroy(lineGameObject);
        }
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
    [Serializable]
    private class SerializationPackage
    {
        public List<SimulationElement> SimulationElements { get; set; }
        public List<LineEntity> LineEntities { get; set; }
    }
}
