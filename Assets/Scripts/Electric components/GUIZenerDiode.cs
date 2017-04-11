using UnityEngine;
using ClassLibrarySharpCircuit;
using Assets.Scripts.Entities;

public class GUIZenerDiode : GUICircuitComponent
{
    private ZenerDiodeEntity _resistorEntity;

    public override SimulationElement Entity
    {
        get
        {
            FillEntity(_resistorEntity);
            return _resistorEntity;
        }
        set
        {
            _resistorEntity = (ZenerDiodeEntity) value;
            TransformFromEntity(_resistorEntity);
        }
    }

    public override void GetProperties()
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        //script.AddNumeric("ResistancePropertyLabel", Resistance.ToString(), Resistance.GetType().ToString(), SetResistance, true, -15.4f, 150.6f);
    }

    public override string GetPropertiesForExport()
    {
        return "<p><span class=\"field-title\">" + "Current " + "</span>" + "TODO" + " [A]" + " </p>";
    }

    // Used for duplicating the components - old component is passes so the new one can copy needed values
    public override void CopyValues(GUICircuitComponent old)
    {
    }

    // Called during instantiation
    public void Awake()
    {
        if (CompareTag("ActiveItem"))
        {
            SetAndInitializeConnectors();
        }
    }

    public override void SetSimulationProp(Circuit sim)
    {
        Debug.Log("activeItem inserted");

        ZenerElm diode = sim.Create<ZenerElm>();

        Connectors[0].DllConnector = diode.leadIn;
        Connectors[1].DllConnector = diode.leadOut;
    }
}
