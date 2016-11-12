using System.IO;
using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;
// Component is meant to be attatched to root element of the whole GUI (canvas)
public class Localization : MonoBehaviour
{
    private ResourceReader _resourceReader;
    private int _languageCycle;
    // instantiate Resource reader and set text elements text from resrouce file
    void Start()
    {
        _resourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName),
            Application.systemLanguage);
        SetElementTexts();
    }
    // used for testing purposes - cycles language between Slovak and English
    public void CycleLanguage()
    {
        ChangeLanguage(_languageCycle++%2 == 0 ? SystemLanguage.Slovak : SystemLanguage.English);
    }
    // create a new instance of resource reader and set text elements text from resrouce file
    public void ChangeLanguage(SystemLanguage language)
    {
        _resourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName),
            language);
        SetElementTexts();
    }
    // cycles trough all child elements and sets their texts according to current language
    private void SetElementTexts()
    {
        foreach (Text textComponent in GetComponentsInChildren<Text>(true))
        {
            textComponent.text = _resourceReader.GetResource(textComponent.name);
        }
    }
}