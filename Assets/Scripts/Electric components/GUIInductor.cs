using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;


public class GUIInductor : GUICircuitComponent
{
    private string _name = "Inductor";
    private InductorEntity _inductorEntity;
    private const double DefaultInductance = 50;
    private const bool DefaultIsTrapezoidal = false;

    public void SetName(string val)
    {
        _name = val;
    }
    
    public double Inductance
    {
        get { return _inductorEntity.Inductance; }
        set { _inductorEntity.Inductance = value; }   // GUI check - accept only positive integer
    }

    public bool IsTrapezoidal
    {
        get { return _inductorEntity.IsTrapezoidal; }
        set { _inductorEntity.IsTrapezoidal = value; }   // GUI check - accept only true/ false
    }

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_inductorEntity);
            return _inductorEntity;
        }
        set
        {
            _inductorEntity = (InductorEntity) value;
            TransformFromEntity(_inductorEntity);
        }
    }

    public void SetInductance(double val)
    {
        Inductance = val;
    }

    public void SetTrapezoidal(bool val)
    {
        IsTrapezoidal = val;
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        script.AddString("ComponentNameLabel", _name, SetName);
        script.AddNumeric("InductancePropertyLabel", Inductance.ToString(), Inductance.GetType().ToString(), SetInductance, false);
        script.AddBoolean("TrapezoidalPropertyLabel", IsTrapezoidal.ToString(), SetTrapezoidal);
        script.AddResult("InductancePropertyLabel", "15.6", "Ohm");
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Inductance " + "</span>" + Inductance + " [H]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
        Inductance = ((GUIInductor) old).Inductance;
        IsTrapezoidal = ((GUIInductor) old).IsTrapezoidal;
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            if (_inductorEntity == null)
            {
                _inductorEntity = new InductorEntity {IsTrapezoidal = DefaultIsTrapezoidal, Inductance = DefaultInductance};
            }
            SetAndInitializeConnectors();
            
            GameObject componentIdManager = GameObject.Find("_ComponentIdManager");
            GenerateId script = componentIdManager.GetComponent<GenerateId>();
            script.generatedIds[4]++;
            _name += script.generatedIds[4].ToString();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        InductorElm inductorElm = sim.Create<InductorElm>();
        inductorElm.inductance = _inductorEntity.Inductance;
        inductorElm.isTrapezoidal = _inductorEntity.IsTrapezoidal;

        Connectors[0].DllConnector = inductorElm.leadIn;
        Connectors[1].DllConnector = inductorElm.leadOut;
    }
}