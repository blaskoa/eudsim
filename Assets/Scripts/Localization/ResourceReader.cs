using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Localization
{
    public class ResourceReader
    {
        private const string XPathLanguageTemplate = "/Languages/{0}";
        private const string AttributeName = "name";
        private const SystemLanguage DefaultLanguage = SystemLanguage.English;
        public static string ResourceFileName = Path.Combine("Scripts\\Localization", "Resources.xml");
        private readonly IDictionary<string, string> _resources;
        public ResourceReader(string resourceFilePath, SystemLanguage language)
        {
            XmlDocument xml = new XmlDocument();
            _resources = new Dictionary<string, string>();
            xml.Load(resourceFilePath);
            if (xml.DocumentElement != null)
            {
                XmlNode languageNode = xml.DocumentElement.SelectSingleNode(string.Format(XPathLanguageTemplate, language));

                if (languageNode != null)
                {
                    foreach (XmlNode languageNodeChildNode in languageNode.ChildNodes)
                    {
                        if (languageNodeChildNode != null && languageNodeChildNode.Attributes != null &&
                            languageNodeChildNode.Attributes[AttributeName] != null)
                        {
                            _resources.Add(languageNodeChildNode.Attributes[AttributeName].InnerText,
                                languageNodeChildNode.InnerText);
                        }
                    }
                }
                else
                {
                    //language not found, using default = english
                    languageNode = xml.DocumentElement.SelectSingleNode(string.Format(XPathLanguageTemplate, DefaultLanguage));
                    if (languageNode != null)
                    {
                        foreach (XmlNode languageNodeChildNode in languageNode.ChildNodes)
                        {
                            if (languageNodeChildNode != null && languageNodeChildNode.Attributes != null &&
                                languageNodeChildNode.Attributes[AttributeName] != null)
                            {
                                _resources.Add(languageNodeChildNode.Attributes[AttributeName].InnerText,
                                    languageNodeChildNode.InnerText);
                            }
                        }
                    }
                }
            }
        }

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
