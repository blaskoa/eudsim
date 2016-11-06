using System.IO;
using Assets.Scripts.Localization;
using UnityEngine;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    private ResourceReader _resourceReader;
	// Use this for initialization
	void Start ()
    {
	    _resourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName), Application.systemLanguage);
        GetComponent<Text>().text = _resourceReader.GetResource(gameObject.name);
    }

    public void ChangeLanguage(SystemLanguage language)
    {
        _resourceReader = new ResourceReader(Path.Combine(Application.dataPath, ResourceReader.ResourceFileName), language);
        GetComponent<Text>().text = _resourceReader.GetResource(gameObject.name);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
