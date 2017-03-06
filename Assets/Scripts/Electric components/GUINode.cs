using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUINode : GUICircuitComponent
{
    private NodeEntity _nodeEntity;
    // Use this for initialization
    public void Start()
    {
        if (CompareTag("ActiveNode"))
        {
            if (_nodeEntity == null)
            {
                _nodeEntity = new NodeEntity();
            }

            Debug.Log("insertol som node");
            SetAndInitializeConnectors();
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

    public override void SetSimulationProp(Circuit sim)
    {
    }
}
