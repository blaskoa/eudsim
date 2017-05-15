using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;
// Component is meant to be attatched to root element of the whole GUI (canvas)
public class Localization : MonoBehaviour
{
    private int _languageCycle;
    // instantiate Resource reader and set text elements text from resrouce file
    void Start()
    {
        ResourceReader.SetLanguage(Application.systemLanguage);
        SetElementTexts();
        
        // initialize
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Slovak:
                SetLanguage("SK");
                break;
            case SystemLanguage.English:
                SetLanguage("EN");
                break;
            default:
                break;
        }
    }
    
    // set language "SK" or "EN"
    public void SetLanguage(string lang)
    {
        GameObject langCaptionText = GameObject.Find("LangCaptionText");
        GameObject langIcon = GameObject.Find("LangIconText");
        GameObject zoomField = GameObject.Find("ZoomText");
        string pom = zoomField.GetComponent<UnityEngine.UI.Text>().text; //workaround for localization erasing calculated zoomText value
        
        switch (lang)
        {
            case "SK":
                ChangeLanguage(SystemLanguage.Slovak);
                langCaptionText.GetComponent<UnityEngine.UI.Text>().text = "Slovenčina";
                langIcon.GetComponent<UnityEngine.UI.Text>().text = "SVK";
                break;
            case "EN":
                ChangeLanguage(SystemLanguage.English);
                langCaptionText.GetComponent<UnityEngine.UI.Text>().text = "English";
                langIcon.GetComponent<UnityEngine.UI.Text>().text = "ENG";
                break;
            default:
                break;
        }
        zoomField.GetComponent<UnityEngine.UI.Text>().text = pom; // set value back
    }
    
    // create a new instance of resource reader and set text elements text from resrouce file
    public void ChangeLanguage(SystemLanguage language)
    {
        ResourceReader.SetLanguage(language);
        SetElementTexts();
    }
    
    // cycles trough all child elements and sets their texts according to current language
    private void SetElementTexts()
    {
        FindObjectOfType<MultiSelect>().DoDeselect();
        foreach (Text textComponent in GetComponentsInChildren<Text>(true))
        {
            textComponent.text = ResourceReader.Instance.GetResource(textComponent.name);
        }
    }
}