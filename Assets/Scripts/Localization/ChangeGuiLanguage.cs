using UnityEngine;

public class ChangeGuiLanguage : MonoBehaviour
{
    private int _cycleCounter = 0;
    public void ChangeGuiLanguageCycle()
    {
        Localization[] localizedElements = FindObjectsOfType<Localization>();
        SystemLanguage newLanguage;
        if (_cycleCounter++ % 2 == 0)
        {
            newLanguage = SystemLanguage.Slovak;
        }
        else
        {
            newLanguage = SystemLanguage.English;
        }

        foreach (Localization localizedElement in localizedElements)
        {
            localizedElement.ChangeLanguage(newLanguage);
        }
    }
}
