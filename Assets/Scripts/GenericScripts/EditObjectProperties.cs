using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class EditObjectProperties : MonoBehaviour
{
    // Attributes for Dynamic Properties Window generation.
    [SerializeField] private GameObject _decimalPrefab;
    [SerializeField] private Toggle _booleanPrefab;

    // Number of fields in the active component
    private int _fieldNum = 0;

    // Change in every anchor and anchor's position for the first property
    private const float AnchorStep = 0.075f;
    private const float FirstAnchor = 0.955f;
    private  GameObject _propertyContent;

    void Start()
    {
        _propertyContent = this.gameObject;
    }

    // CLear all property fields.
    public void Clear()
    {
        foreach (Transform child in _propertyContent.transform)
        {
            Destroy(child.gameObject);
        }
        _fieldNum = 0;
    }

    // Add a numeric property field - validationType can be either "Integer" or "Double" and it affects the input validation used
    public void AddNumeric(string resourceKey, string value, string validationType, Action<double> set )
    {
        // Instantiate new Property and set its anchor
        GameObject newProperty = Instantiate(_decimalPrefab);
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_decimalPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_decimalPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set property label
        Text propertyLabel = newProperty.transform.FindChild("ObjectPropertyLabel").gameObject.GetComponent<Text>();
        string labelValue = FindObjectOfType<Localization>().ResourceReader.GetResource(resourceKey);
        propertyLabel.text = labelValue;
        
        // Set property InputField
        GameObject inputFieldGo = newProperty.transform.FindChild("InputField").gameObject;
        InputField inputField = inputFieldGo.GetComponent<InputField>();
        inputField.text = value;
        if (validationType.Contains("Integer"))
        {
            inputField.characterValidation = InputField.CharacterValidation.Integer;
        }
        else
        {
            inputField.characterValidation = InputField.CharacterValidation.Decimal;
        }

        // Set method to be run when the editing is finished
        inputField.onEndEdit.AddListener(delegate { set(Double.Parse(inputField.text)); });

        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);
        
        _fieldNum++;
    }

    // Add a boolean Property, takes function "set" as argument which will set which will be run when toggle is changed
    public void AddBoolean(string resourceKey, string value, Action<bool> set)
    {
        Toggle newProperty = Instantiate(_booleanPrefab);
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.name = resourceKey;
        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_booleanPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_booleanPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set Boolean Property Label
        Text propertyLabel = newProperty.transform.FindChild("Label").gameObject.GetComponent<Text>();
        string labelValue = FindObjectOfType<Localization>().ResourceReader.GetResource(resourceKey);
        propertyLabel.text = labelValue;

        // Setting Toggle value
        newProperty.isOn = value == "True";

        // When the user click on toggle
        newProperty.onValueChanged.AddListener( delegate { set(newProperty.isOn); });

        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);

        _fieldNum++;
    }
}
