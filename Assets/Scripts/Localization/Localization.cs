using System.IO;
using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;
// Component is meant to be attatched to root element of the whole GUI (canvas)
public class Localization : MonoBehaviour
{
    public ResourceReader ResourceReader { get; private set; }
    private int _languageCycle;
    // instantiate Resource reader and set text elements text from resrouce file
    void Start()
    {
        ResourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName),
            Application.systemLanguage);
        SetElementTexts();
	    if (Application.systemLanguage.ToString() == "Slovak")
		{
			_languageCycle = _languageCycle + 1;
		}
    }
    // used for testing purposes - cycles language between Slovak and English
    public void CycleLanguage()
    {
        ChangeLanguage(_languageCycle++%2 == 0 ? SystemLanguage.Slovak : SystemLanguage.English);
    }
    // create a new instance of resource reader and set text elements text from resrouce file
    public void ChangeLanguage(SystemLanguage language)
    {
        ResourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName),
            language);
        SetElementTexts();
    }
    // cycles trough all child elements and sets their texts according to current language
    private void SetElementTexts()
    {
        FindObjectOfType<Deselect>().DoDeselect();
        foreach (Text textComponent in GetComponentsInChildren<Text>(true))
        {
            textComponent.text = ResourceReader.GetResource(textComponent.name);
        }
    }
}