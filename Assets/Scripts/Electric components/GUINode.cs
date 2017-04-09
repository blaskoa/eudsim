using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUINode : GUICircuitComponent
{
    private string _name = "Node";
    private NodeEntity _nodeEntity;
    
    public void SetName(string val)
    {
        _name = val;
    }
    
    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveNode"))
        {
            if (_nodeEntity == null)
            {
                _nodeEntity = new NodeEntity();
            }

            Debug.Log("insertol som node");
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[5]++;
            _name += script.generatedIds[5].ToString();
        }
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_nodeEntity);
            return _nodeEntity;
        }
        set
        {
            _nodeEntity = (NodeEntity) value;
            TransformFromEntity(_nodeEntity);
        }
    }

    public override void GetProperties()
    {
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "GUI Node " + "</span>" + "Nothing to show" + " [N/A]" + " </p>";
    }

    public override void SetSimulationProp(Circuit sim)
    {
    }
}
