using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Localization
{
    // Class that parses XML resource file and generates a string dictionary with localized values
    // text element names are used as keys
    public class ResourceReader
    {
        private const string XPathLanguageTemplate = "/Languages/{0}";
        private const string AttributeName = "name";
        private const SystemLanguage DefaultLanguage = SystemLanguage.English;
        private readonly IDictionary<string, string> _resources;
        
        public ResourceReader(SystemLanguage language)
        {
            XmlDocument xml = new XmlDocument();
            _resources = new Dictionary<string, string>();
            TextAsset textAsset = (TextAsset)Resources.Load("Resources", typeof(TextAsset));
            xml.LoadXml(textAsset.text);
            if (xml.DocumentElement != null)
            {
                // selects requested laguage node if requested language is not found, use default = english
                XmlNode languageNode = xml.DocumentElement.SelectSingleNode(string.Format(XPathLanguageTemplate, language)) ??
                                       xml.DocumentElement.SelectSingleNode(string.Format(XPathLanguageTemplate, DefaultLanguage));

                if (languageNode != null)
                {
                    // iterates through child nodes of language nodes
                    foreach (XmlNode languageNodeChildNode in languageNode.ChildNodes)
                    {
                        if (languageNodeChildNode != null && languageNodeChildNode.Attributes != null &&
                            languageNodeChildNode.Attributes[AttributeName] != null)
                        {
                            // fills the resource dictionary with name attribute as key and element text value as value
                            _resources.Add(languageNodeChildNode.Attributes[AttributeName].InnerText,
                                languageNodeChildNode.InnerText);
                        }
                    }
                }
            }
        }
        // gets resource by key passed as argument
        public string GetResource(string key)
        {
            string result;
            try
            {
                result = _resources[key];
            }
            catch (KeyNotFoundException)
            {
                result = string.Empty;
            }
            return result;
        }
    }
}
