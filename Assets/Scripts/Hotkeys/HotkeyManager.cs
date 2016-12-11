using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Hotkeys
{
    public class HotkeyManager
    {
        private const string XPathHotkeys = "/Hotkeys";
        private const string AttributeName = "name";
        private const string AttributeModifier = "modifier";
        private static HotkeyManager _instance;
        public static string HotkeyFileName = Path.Combine(Application.dataPath, "Scripts\\Hotkeys\\Hotkeys.xml");
        private readonly IDictionary<string, Hotkey> _hotkeyDictionary;

        private HotkeyManager(string hotkeyFilePath)
        {
            XmlDocument xml = new XmlDocument();
            _hotkeyDictionary = new Dictionary<string, Hotkey>();
            xml.Load(hotkeyFilePath);
            if (xml.DocumentElement != null)
            {
                XmlNode hotkeysNode = xml.DocumentElement.SelectSingleNode(XPathHotkeys);

                if (hotkeysNode != null)
                    foreach (XmlNode hotkeyNode in hotkeysNode.ChildNodes)
                        if ((hotkeyNode != null) && (hotkeyNode.Attributes != null) &&
                            (hotkeyNode.Attributes[AttributeName] != null))
                        {
                            KeyCode? modifier = null;
                            // fills the hotkey dictionary with name attribute as key and element text value as value
                            if (hotkeyNode.Attributes[AttributeModifier] != null)
                                modifier = (KeyCode?) int.Parse(hotkeyNode.Attributes[AttributeModifier].InnerText);

                            _hotkeyDictionary.Add(hotkeyNode.Attributes[AttributeName].InnerText,
                                new Hotkey((KeyCode) int.Parse(hotkeyNode.InnerText), modifier));
                        }
            }
        }

        public static HotkeyManager Instance
        {
            get { return _instance ?? (_instance = new HotkeyManager(HotkeyFileName)); }
        }

        public bool CheckHotkey(string key, KeyAction action = KeyAction.Pressed)
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
            if (result && (hotkey != null))
            {
                if (hotkey.Modifier.HasValue)
                {
                    result = Input.GetKey(hotkey.Modifier.Value);
                }
                switch (action)
                {
                    case KeyAction.Pressed:
                    {
                        result = result && Input.GetKey(hotkey.KeyCode);
                        break;
                    }
                    case KeyAction.Down:
                    {
                        result = result && Input.GetKeyDown(hotkey.KeyCode);
                        break;
                    }
                    case KeyAction.Up:
                    {
                        result = result && Input.GetKeyUp(hotkey.KeyCode);
                        break;
                    }
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
                result = hotkey.ToString();
            }
            return result;
        }

        private class Hotkey
        {
            public Hotkey(KeyCode keyCode, KeyCode? modifier)
            {
                KeyCode = keyCode;
                Modifier = modifier;
            }

            public KeyCode KeyCode { get; private set; }
            public KeyCode? Modifier { get; private set; }

            public override string ToString()
            {
                return (Modifier.HasValue ? Modifier.Value + " + " : "") + KeyCode;
            }
        }
    }
}
