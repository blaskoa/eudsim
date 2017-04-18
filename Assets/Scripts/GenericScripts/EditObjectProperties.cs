using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Localization;

public class EditObjectProperties : MonoBehaviour
{
    // Attributes for Dynamic Properties Window generation.
    [SerializeField] private GameObject _decimalPrefab;
    [SerializeField] private Toggle _booleanPrefab;
    [SerializeField] private GameObject _sliderPrefab;
    [SerializeField] private GameObject _resultPrefab;
    [SerializeField] private GameObject _stringPrefab;

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
    public void AddNumeric(string resourceKey, string value, string validationType, Action<double> set, bool useSlider, float min = 0, float max = 100 )
    {
        // Instantiate new Property and set its anchor
        GameObject newProperty;
        if (useSlider)
        {
            newProperty = Instantiate(_sliderPrefab);
        }
        else
        {
            newProperty = Instantiate(_decimalPrefab);
        }
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_decimalPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_decimalPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set property label
        Text propertyLabel = newProperty.transform.FindChild("ObjectPropertyLabel").gameObject.GetComponent<Text>();
        string labelValue = ResourceReader.Instance.GetResource(resourceKey);
        propertyLabel.text = labelValue;
        
        // Set property Slider
        if (useSlider)
        {
            GameObject sliderGo = newProperty.transform.FindChild("Slider").gameObject;
            Slider slider = sliderGo.GetComponent<Slider>();
            InputField sliderValueInputField = newProperty.transform.FindChild("SliderValue").gameObject.GetComponent<InputField>();
            
            // Character validation
            sliderValueInputField.characterValidation = validationType.Contains("Integer") ? InputField.CharacterValidation.Integer : InputField.CharacterValidation.Decimal;
            // Update slider when value is edited.
            sliderValueInputField.onEndEdit.AddListener(delegate { slider.value = float.Parse(sliderValueInputField.text); });

            // Set slider
            slider.minValue = min;
            slider.maxValue = max;
            slider.wholeNumbers = validationType.Contains("Integer");
            slider.value = float.Parse(value);
            sliderValueInputField.text = slider.value.ToString();

            // Set method to be run when the slider is moved
            slider.onValueChanged.AddListener(delegate
            {
                set(slider.value);
                sliderValueInputField.text = slider.value.ToString();
            });
        }
        // Set property InputField
        else
        {
            GameObject inputFieldGo = newProperty.transform.FindChild("InputField").gameObject;
            InputField inputField = inputFieldGo.GetComponent<InputField>();
            inputField.text = value;
            inputField.characterValidation = validationType.Contains("Integer") ? InputField.CharacterValidation.Integer : InputField.CharacterValidation.Decimal;

            // Set method to be run when the editing is finished
            inputField.onEndEdit.AddListener(delegate { set(Double.Parse(inputField.text)); });
        }

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
        string labelValue = ResourceReader.Instance.GetResource(resourceKey);
        propertyLabel.text = labelValue;

        // Setting Toggle value
        newProperty.isOn = value == "True";

        // When the user click on toggle
        newProperty.onValueChanged.AddListener( delegate { set(newProperty.isOn); });

        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);

        _fieldNum++;
    }

    // Add a result property without editable fields
    public void AddResult(string resourceKey, string value, string unit = "")
    {
        GameObject newProperty = Instantiate(_resultPrefab);
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_resultPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_resultPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set Property Label
        Text propertyLabel = newProperty.transform.FindChild("Label").gameObject.GetComponent<Text>();
        string labelValue = ResourceReader.Instance.GetResource(resourceKey);
        propertyLabel.text = labelValue;

        // Set Property Value
        Text propertyValue = newProperty.transform.FindChild("Value").gameObject.GetComponent<Text>();
        float numericVal = float.Parse(value);
        propertyValue.text = Math.Round(numericVal, 3) + " " + unit;

        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);
    }
    
    // Add a string property field
    public void AddString(string resourceKey, string value, Action<string> set)
    {
        // Instantiate new Property and set its anchor
        GameObject newProperty = Instantiate(_stringPrefab);;
        float anchorPosition = FirstAnchor - _fieldNum * AnchorStep;

        newProperty.GetComponent<RectTransform>().anchorMin = new Vector2(_stringPrefab.GetComponent<RectTransform>().anchorMin.x, anchorPosition);
        newProperty.GetComponent<RectTransform>().anchorMax = new Vector2(_stringPrefab.GetComponent<RectTransform>().anchorMax.x, anchorPosition);

        // Set property label
        Text propertyLabel = newProperty.transform.FindChild("ObjectPropertyLabel").gameObject.GetComponent<Text>();
        string labelValue = ResourceReader.Instance.GetResource(resourceKey);
        propertyLabel.text = labelValue;
        
        GameObject inputFieldGo = newProperty.transform.FindChild("InputField").gameObject;
        InputField inputField = inputFieldGo.GetComponent<InputField>();
        inputField.text = value;

        // Set method to be run when the editing is finished
        inputField.onEndEdit.AddListener(delegate { set(inputField.text); });

        // Add newly created property to the UI
        newProperty.transform.SetParent(_propertyContent.transform, false);
        
        _fieldNum++;
    }
}
