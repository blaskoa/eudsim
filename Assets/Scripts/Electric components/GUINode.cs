using UnityEngine;
using Assets.Scripts.Entities;
using ClassLibrarySharpCircuit;

public class GUINode : GUICircuitComponent
{
    private NodeEntity _nodeEntity;
    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveNode"))
        {
            if (_nodeEntity == null)
            {
                _nodeEntity = new NodeEntity();
            }
            
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

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "GUI Node " + "</span>" + "Nothing to show" + " [N/A]" + " </p>";
    }

    public override void SetSimulationProp(Circuit sim)
    {
    }
}
