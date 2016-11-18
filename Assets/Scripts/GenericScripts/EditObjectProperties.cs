using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EditObjectProperties : MonoBehaviour
{
    // Attributes for Dynamic Properties Window generation.
    private static GameObject _propertyPrefab;
    [SerializeField] private GameObject _prefab;

    // Number of fields in the active component
    private static int _fieldNum = 0;

    // Change in every anchor and anchor's position for the first property
    private const float AnchorStep = 0.075f;
    private const float FirstAnchor = 0.955f;
    private static GameObject _propertyContent;

    void Start()
    {
        _propertyContent = GameObject.Find("PropertiesWindowContainer");
        _propertyPrefab = _prefab;
    }

    // CLear all property fields.
    public static void Clear()
    {
        foreach (Transform child in _propertyContent.transform)
        {
            Destroy(child.gameObject);
        }
        _fieldNum = 0;
    }

    // Add a property field.
    public static void Add(string label, string value)
    {
        // Instantiate new Property and set its anchor
        GameObject newProperty = Instantiate(_propertyPrefab);
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_propertyPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_propertyPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set property label
        Text propertyLabel = newProperty.transform.FindChild("ObjectPropertyLabel").gameObject.GetComponent<Text>();
        propertyLabel.text = label;

        // Set property InputField
        GameObject inputFieldGo = newProperty.transform.FindChild("InputField").gameObject;
        InputField inputField = inputFieldGo.GetComponent<InputField>();
        inputField.text = value;

        // Set method to be run when the editing is finished
        Component2 script = SelectObject.SelectedObject.GetComponent<Component2>();
        inputField.onEndEdit.AddListener(delegate { script.setProperties(); });
        
        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);

        _fieldNum++;
    }

    // Read User Input
    public static List<string> Get()
    {
        List<string> values = new List<string>();
        foreach (Transform property in _propertyContent.transform)
        {
            GameObject inputFieldGo = property.transform.FindChild("InputField").gameObject;
            InputField inputField = inputFieldGo.GetComponent<InputField>();
            values.Add(inputField.text);
        }

        return values;
    }
}
