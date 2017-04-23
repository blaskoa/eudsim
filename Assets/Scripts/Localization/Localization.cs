using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;
// Component is meant to be attatched to root element of the whole GUI (canvas)
public class Localization : MonoBehaviour
{
    // instantiate Resource reader and set text elements text from resrouce file
    void Start()
    {
        ResourceReader.SetLanguage(Application.systemLanguage);
        SetElementTexts();
        SetLanguage(Application.systemLanguage);
    }
    
    // set language "SK" or "EN"
    private void SetLanguage(SystemLanguage language)
    {
        GameObject captionSK = GameObject.Find("CaptionSlovakText");
        GameObject captionEN = GameObject.Find("CaptionEnglishText");
        GameObject iconSK = GameObject.Find("IconSKText");
        GameObject iconEN = GameObject.Find("IconENText");
        GameObject zoomField = GameObject.Find("ZoomText");
        string pom = zoomField.GetComponent<Text>().text; //workaround for localization erasing calculated zoomText value
        
        switch (language)
        {
            case SystemLanguage.Slovak:
                ChangeLanguage(SystemLanguage.Slovak);
                captionSK.GetComponent<Text>().enabled = true;
                captionEN.GetComponent<Text>().enabled = false;
                iconSK.GetComponent<Text>().enabled = true;
                iconEN.GetComponent<Text>().enabled = false;
                break;
            case SystemLanguage.English:
                ChangeLanguage(SystemLanguage.English);
                captionSK.GetComponent<Text>().enabled = false;
                captionEN.GetComponent<Text>().enabled = true;
                iconSK.GetComponent<Text>().enabled = false;
                iconEN.GetComponent<Text>().enabled = true;
                break;
        }
        zoomField.GetComponent<Text>().text = pom; // set value back
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