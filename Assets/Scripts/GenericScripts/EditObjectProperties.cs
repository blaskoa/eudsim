using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EditObjectProperties : MonoBehaviour
{
    // Attributes for Dynamic Properties Window generation.
    private static GameObject _propertyPrefab;
    [SerializeField] private GameObject _prefab;
    private static int _fieldNum = 0;
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
        GameObject newProperty = Instantiate(_propertyPrefab);
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_propertyPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_propertyPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        newProperty.transform.FindChild("ObjectPropertyLabel").gameObject.GetComponent<Text>().text = label;
        GameObject inputFieldGO = newProperty.transform.FindChild("InputField").gameObject;
        InputField inputField = inputFieldGO.GetComponent<InputField>();
        inputField.text = value;
        
        newProperty.transform.SetParent(_propertyContent.transform, false);

        _fieldNum++;
    }
}
