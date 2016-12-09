using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Hotkeys
{
    public class HotkeyManager
    {
        private static HotkeyManager _instance;
        private const string XPathHotkeys = "/Hotkeys";
        private const string AttributeName = "name";
        private const string AttributeModifier = "modifier";
        private readonly IDictionary<string, Hotkey> _hotkeyDictionary;
        public static string HotkeyFileName = Path.Combine(Application.dataPath, "Scripts\\Hotkeys\\Hotkeys.xml");

        public static HotkeyManager Instance
        {
            get { return _instance ?? (_instance = new HotkeyManager(HotkeyFileName)); }
        }

        private HotkeyManager(string hotkeyFilePath)
        {
            XmlDocument xml = new XmlDocument();
            _hotkeyDictionary = new Dictionary<string, Hotkey>();
            xml.Load(hotkeyFilePath);
            if (xml.DocumentElement != null)
            {
                // selects requested laguage node if requested language is not found, use default = english
                XmlNode hotkeysNode = xml.DocumentElement.SelectSingleNode(XPathHotkeys);

                if (hotkeysNode != null)
                {
                    // iterates through child nodes of language nodes
                    foreach (XmlNode hotkeyNode in hotkeysNode.ChildNodes)
                    {
                        if (hotkeyNode != null && hotkeyNode.Attributes != null &&
                            hotkeyNode.Attributes[AttributeName] != null)
                        {
                            KeyCode? modifier = null;
                            // fills the resource dictionary with name attribute as key and element text value as value
                            if (hotkeyNode.Attributes[AttributeModifier] != null)
                            {
                                modifier = (KeyCode?)int.Parse(hotkeyNode.Attributes[AttributeModifier].InnerText);
                            }

                            _hotkeyDictionary.Add(hotkeyNode.Attributes[AttributeName].InnerText,
                                new Hotkey((KeyCode) int.Parse(hotkeyNode.InnerText), modifier));
                        }
                    }
                }
            }
        }
        // gets resource by key passed as argument
        public bool CheckHotkey(string key)
        {
            Hotkey hotkey = null;
            bool result = true;
            try
            {
                hotkey = _instance._hotkeyDictionary[key];
            }
            catch (KeyNotFoundException)
            {
                result = false;
            }
            if (result && hotkey != null)
            {
                if (hotkey.Modifier.HasValue)
                {
                    result = Input.GetKey(hotkey.Modifier.Value) && Input.GetKey(hotkey.KeyCode);
                }
                else
                {
                    result = Input.GetKey(hotkey.KeyCode);
                }
            }
            return result;
        }

        public bool CheckHotkeyDown(string key)
        {
            Hotkey hotkey = null;
            bool result = true;
            try
            {
                hotkey = _instance._hotkeyDictionary[key];
            }
            catch (KeyNotFoundException)
            {
                result = false;
            }
            if (result && hotkey != null)
            {
                if (hotkey.Modifier.HasValue)
                {
                    result = Input.GetKey(hotkey.Modifier.Value) && Input.GetKeyDown(hotkey.KeyCode);
                }
                else
                {
                    result = Input.GetKeyDown(hotkey.KeyCode);
                }
            }
            return result;
        }

        public bool CheckHotkeyUp(string key)
        {
            Hotkey hotkey = null;
            bool result = true;
            try
            {
                hotkey = _instance._hotkeyDictionary[key];
            }
            catch (KeyNotFoundException)
            {
                result = false;
            }
            if (result && hotkey != null)
            {
                if (hotkey.Modifier.HasValue)
                {
                    result = Input.GetKey(hotkey.Modifier.Value) && Input.GetKeyUp(hotkey.KeyCode);
                }
                else
                {
                    result = Input.GetKeyUp(hotkey.KeyCode);
                }
            }
            return result;
        }

        public string GetHotkeyLabel(string key)
        {
            Hotkey hotkey = null;
            string result = null;
            try
            {
                hotkey = _instance._hotkeyDictionary[key];
            }
            catch (KeyNotFoundException)
            {
                result = string.Empty;
            }
            if (hotkey != null)
            {
                result = (hotkey.Modifier.HasValue?hotkey.Modifier.Value + " + ":"") + hotkey.KeyCode;
            }
            return result;
        }
        private class Hotkey
        {
            public KeyCode KeyCode { get; private set; }
            public KeyCode? Modifier { get; private set; }

            public Hotkey(KeyCode keyCode, KeyCode? modifier)
            {
                KeyCode = keyCode;
                Modifier = modifier;
            }
        }
    }
}
