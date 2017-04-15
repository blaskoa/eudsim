using System;
using System.Collections;
using System.Collections.Generic;
using ClassLibrarySharpCircuit;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Assets.Scripts.Utils;
using Ionic.Zip;

public class ExportHTML : MonoBehaviour
{
    public Camera Camera;

    public void Awake()
    {
        FileBrowserHandler.Instance.ExportScript = this;
    }

    private string _zipFileName;

    public string ZipFileName
    {
        get { return _zipFileName; }
        private set { _zipFileName = value.Trim(); }
    }

    // Exports the current scheme to HTML5
    public void MakeHtmlExport(string fileName)
    {
        // Base folder where all export files are located
        const string baseFolder = "EduSimExport";
        const string javascriptPattern = "js/edusim-pattern.js";
        const string javascriptExport = "js/edusim.js";

        if (!string.IsNullOrEmpty(fileName))
        {
            ZipFileName = fileName;
        }
        else if (string.IsNullOrEmpty(ZipFileName))
        {
            throw new ArgumentNullException("fileName");
        }

        //because canvas to which we make export is printing in another quadrant we need to make rotation
        Camera.transform.rotation *= Quaternion.Euler(180, 0, 0);
        List<string> exportArrayList = new List<string>();
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag.Equals("ActiveItem") || obj.tag.Equals("ActiveNode"))
            {
                float x = obj.transform.position.x;
                float y = obj.transform.position.y;
                string imageName = "images/bulb.png"; //make bulb as default image
                if (obj.name.Contains("Node"))
                    imageName = "images/Node.png";
                else if (obj.name.Contains("Bulb"))
                    imageName = "images/bulb.png";
                else if (obj.name.Contains("Ampermeter"))
                    imageName = "images/ampermeter.png";
                else if (obj.name.Contains("Voltmeter"))
                    imageName = "images/voltmeter.png";
                else if (obj.name.Contains("Switch"))
                    imageName = "images/switch.png";
                else if (obj.name.Contains("Accumulator"))
                    imageName = "images/accumulator.png";
                else if (obj.name.Contains("Capacitor"))
                    imageName = "images/capacitor.png";
                else if (obj.name.Contains("Coil"))
                    imageName = "images/coil.png";
                else if (obj.name.Contains("Resistor"))
                    imageName = "images/resistor.png";
                else if (obj.name.Contains("TransistorPNP"))
                    imageName = "images/PNPTranzistor.png";
                else if (obj.name.Contains("TransistorNPN"))
                    imageName = "images/NPNTranzistor.png";
                else if (obj.name.Contains("ZenerDiode"))
                    imageName = "images/ZenerDiode.png";
                else if (obj.name.Contains("LedDiode"))
                    imageName = "images/LedDiode.png";
                

                Vector3 screenPos = Camera.WorldToScreenPoint(obj.transform.position);

                exportArrayList.Add("{x:" + screenPos.x + ",y:" + screenPos.y + ",radius:50, rotate:" +
                         obj.GetComponent<GUICircuitComponent>().transform.rotation.eulerAngles.z + ", img:'" +
                         imageName + "' , componentName: '" + obj.GetComponent<GUICircuitComponent>().GetPropertiesForExport() + "'},");

                //and connctros
                screenPos =
                    Camera.WorldToScreenPoint(obj.GetComponent<GUICircuitComponent>().Connectors[0].transform.position);
                exportArrayList.Add("{x:" + screenPos.x + ",y:" + screenPos.y +
                         ",radius:7, img:'images/connector.png',componentName:'n/a'},");

                if (obj.name.Contains("Node") == false)
                //as this is so far only way how to determine count of connectors we relay on name
                {
                    screenPos =
                        Camera.WorldToScreenPoint(
                            obj.GetComponent<GUICircuitComponent>().Connectors[1].transform.position);
                    exportArrayList.Add("{x:" + screenPos.x + ",y:" + screenPos.y +
                        ",radius:7, img:'images/connector.png',componentName:'n/a'},");
                }

                if (obj.name.Contains("Transistor") == true)
                //as this is so far only way how to determine count of connectors we relay on name
                {
                    screenPos =
                        Camera.WorldToScreenPoint(
                            obj.GetComponent<GUICircuitComponent>().Connectors[2].transform.position);
                    exportArrayList.Add("{x:" + screenPos.x + ",y:" + screenPos.y +
                        ",radius:7, img:'images/connector.png',componentName:'n/a'},");
                }
            }
            if (obj.tag.Equals("ActiveLine") && obj.name.Contains("(Clone)"))
            {
                Vector3 screenPosBegin = Camera.WorldToScreenPoint(obj.GetComponent<Line>().StartPos);
                Vector3 screenPosMiddle = Camera.WorldToScreenPoint(obj.GetComponent<Line>().MiddlePos);
                Vector3 screenPosEnd = Camera.WorldToScreenPoint(obj.GetComponent<Line>().EndPos);
                if (obj.GetComponent<Line>().TypeOfLine != "NoBreak")
                {
                    exportArrayList.Add("{x:" + screenPosBegin.x + ",y:" + screenPosBegin.y + ",z:" + screenPosMiddle.x + ",q:" +
                             screenPosMiddle.y + ",radius:50, img:'images/wire.png',componentName:'n/a'},");
                    exportArrayList.Add("{x:" + screenPosMiddle.x + ",y:" + screenPosMiddle.y + ",z:" + screenPosEnd.x + ",q:" +
                             screenPosEnd.y + ",radius:50, img:'images/wire.png',componentName:'n/a'},");
                }
                else
                {
                    exportArrayList.Add("{x:" + screenPosBegin.x + ",y:" + screenPosBegin.y + ",z:" + screenPosEnd.x + ",q:" +
                             screenPosEnd.y + ",radius:50, img:'images/wire.png',componentName:'n/a'},");
                }
            }
        }
        //and now make rotation to previous position
        Camera.transform.rotation *= Quaternion.Euler(180, 0, 0);
        string text = File.ReadAllText(Path.Combine(baseFolder, javascriptPattern));
        string insertPoint = "let hotspots = [";
        int index = text.IndexOf(insertPoint, StringComparison.Ordinal) + insertPoint.Length;
        text = text.Insert(index, string.Join("", exportArrayList.ToArray()));
        File.WriteAllText(Path.Combine(baseFolder, javascriptExport), text);

        // Create a ZIP archive of the export
        string subfolder = "";
        string[] files = {};
        ZipFile zip = new ZipFile();

        // Add base folder to the archive
        files = Directory.GetFiles(baseFolder, "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, baseFolder);

        // Add css to the archive
        subfolder = "css";
        files = Directory.GetFiles(Path.Combine(baseFolder, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Add JavaScript scripts to the archive
        subfolder = "js";
        files = Directory.GetFiles(Path.Combine(baseFolder, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Add images to the archive
        subfolder = "images";
        files = Directory.GetFiles(Path.Combine(baseFolder, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Add fonts to the archive
        subfolder = "fonts";
        files = Directory.GetFiles(Path.Combine(baseFolder, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Save the archive
        zip.Save(ZipFileName);

        this.GetComponent<Whisp>().Say("HTML export was successfully saved into " + ZipFileName + ".");
    }
}
