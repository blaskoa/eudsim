using System;
using System.Collections;
using System.Collections.Generic;
using ClassLibrarySharpCircuit;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
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

    // Limit the distance from the component - usually to half radius, used for components and lines during export
    private Vector3 _getRelativePosition(Vector3 absPositon, Vector3 componentPosition, int maxDistance)
    {
        Vector3 relativePosition = absPositon;
        if (absPositon.x < componentPosition.x)
        {
            relativePosition.x = componentPosition.x - maxDistance;
        }
        else if (absPositon.x > componentPosition.x)
        {
            relativePosition.x = componentPosition.x + maxDistance;
        }

        if (absPositon.y < componentPosition.y)
        {
            relativePosition.y = componentPosition.y - maxDistance;
        }
        else if (absPositon.y > componentPosition.y)
        {
            relativePosition.y = componentPosition.y + maxDistance;
        }

        return relativePosition;
    }

    // Exports the current scheme to HTML5
    public void MakeHtmlExport(string fileName)
    {
        // Base folder where all export files are located
        string baseFolder = "EduSimExport";
        const string javascriptPattern = "js/edusim-pattern.js";
        const string javascriptExport = "js/edusim.js";
        const int radius = 36;
        const int halfRadius = radius / 2;

        string pathToExport = System.IO.Path.Combine(Application.streamingAssetsPath, baseFolder);

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
            if (obj.tag.Equals("ActiveItem") || obj.tag.Equals("ActiveNode") || obj.tag.Equals("Arrow"))
            {
                float x = obj.transform.position.x;
                float y = obj.transform.position.y;

                // Set image to be exported
                string imageName;
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
                else if (obj.name.Contains("Potentiometer"))
                    imageName = "images/potentiometer.png";
                else if (obj.name.Contains("TransistorPNP"))
                    imageName = "images/PNPTranzistor.png";
                else if (obj.name.Contains("TransistorNPN"))
                    imageName = "images/NPNTranzistor.png";
                else if (obj.name.Contains("ZenerDiode"))
                    imageName = "images/ZenerDiode.png";
                else if (obj.name.Contains("LedDiode"))
                    imageName = "images/LedDiode.png";
                else if (obj.name.Contains("Arrow"))
                {
                    if (obj.GetComponentInParent<GUICircuitComponent>().tag.Equals("ActiveItem")) //do not make export from toolbox
                    {
                        imageName = "images/PotentiometerArrow.png";
                        Vector3 screenPos2 = Camera.WorldToScreenPoint(obj.transform.position);

                        exportArrayList.Add("{x:" + screenPos2.x + ",y:" + screenPos2.y + ",radius:0, rotate:" +
                                            -obj.transform.rotation.eulerAngles.z + ", img:'" +
                                            imageName + "' , componentName: '" + "no properties" + "'},");
                    }
                    continue;
                    //as this is just arrow continue without connectors

                }
                else
                    imageName = "images/bulb.png";

                // Get Screen position of the component
                Vector3 screenPos = Camera.WorldToScreenPoint(obj.transform.position);

                exportArrayList.Add("{x:" + screenPos.x + ",y:" + screenPos.y + ",radius:" + radius.ToString() + ", rotate:" +
                         -obj.GetComponent<GUICircuitComponent>().transform.rotation.eulerAngles.z + ", img:'" +
                         imageName + "' , componentName: '" + obj.GetComponent<GUICircuitComponent>().GetPropertiesForExport() + "'},");

                // Export component's Connectors
                List<Connector> connectors = obj.GetComponent<GUICircuitComponent>().Connectors;
                foreach (Connector conn in connectors)
                {
                    Vector3 connectorScreenPosition =
                        Camera.WorldToScreenPoint(conn.transform.position);
                    connectorScreenPosition = _getRelativePosition(connectorScreenPosition, screenPos, halfRadius);

                    exportArrayList.Add("{x:" + connectorScreenPosition.x + ",y:" + connectorScreenPosition.y +
                             ",radius:7, img:'images/connector.png',componentName:'n/a'},");
                }
            }

            if (obj.tag.Equals("ActiveLine") && obj.name.Contains("(Clone)"))
            {
                // Get Line and calculate it's relative position same as for the connectors
                Line line = obj.GetComponent<Line>();
                Vector3 screenPosBegin = Camera.WorldToScreenPoint(line.Begin.transform.position);
                Vector3 componentPos = line.Begin.transform.parent.position;
                componentPos = Camera.WorldToScreenPoint(componentPos);
                screenPosBegin = _getRelativePosition(screenPosBegin, componentPos, halfRadius);
                
                Vector3 screenPosEnd = Camera.WorldToScreenPoint(line.End.transform.position);
                componentPos = line.End.transform.parent.position;
                componentPos = Camera.WorldToScreenPoint(componentPos);
                screenPosEnd = _getRelativePosition(screenPosEnd, componentPos, halfRadius);
                
                Vector3 screenPosMiddle = Camera.WorldToScreenPoint(line.MiddlePos);
                screenPosMiddle.x = line.MiddlePos.x == line.StartPos.x ? screenPosBegin.x : screenPosEnd.x;
                screenPosMiddle.y = line.MiddlePos.y == line.StartPos.y ? screenPosBegin.y : screenPosEnd.y;

                // Line has a middle point - it's broken
                if (line.TypeOfLine != "NoBreak")
                {
                    exportArrayList.Add("{x:" + screenPosBegin.x + ",y:" + screenPosBegin.y + ",z:" + screenPosMiddle.x + ",q:" +
                             screenPosMiddle.y + ",radius:" + radius.ToString() + ", img:'images/wire.png',componentName:'n/a'},");
                    exportArrayList.Add("{x:" + screenPosMiddle.x + ",y:" + screenPosMiddle.y + ",z:" + screenPosEnd.x + ",q:" +
                             screenPosEnd.y + ",radius:" + radius.ToString() + ", img:'images/wire.png',componentName:'n/a'},");
                }
                else
                {
                    exportArrayList.Add("{x:" + screenPosBegin.x + ",y:" + screenPosBegin.y + ",z:" + screenPosEnd.x + ",q:" +
                             screenPosEnd.y + ",radius:" + radius.ToString() + ", img:'images/wire.png',componentName:'n/a'},");
                }
            }
        }
        //and now make rotation to previous position
        Camera.transform.rotation *= Quaternion.Euler(180, 0, 0);
        string edusimJs = File.ReadAllText(Path.Combine(pathToExport, javascriptPattern));
        string insertPoint = "let hotspots = [";
        int index = edusimJs.IndexOf(insertPoint, StringComparison.Ordinal) + insertPoint.Length;
        edusimJs = edusimJs.Insert(index, string.Join("", exportArrayList.ToArray()));

        // Create a ZIP archive of the export
        string subfolder = "";
        string[] files = {};
        ZipFile zip = new ZipFile();

        // Add base folder to the archive
        files = Directory.GetFiles(pathToExport, "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, baseFolder);

        // Add css to the archive
        subfolder = "css";
        files = Directory.GetFiles(Path.Combine(pathToExport, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Add JavaScript scripts to the archive
        subfolder = "js";
        files = Directory.GetFiles(Path.Combine(pathToExport, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));
        zip.AddEntry(Path.Combine(baseFolder, javascriptExport), edusimJs);

        // Add images to the archive
        subfolder = "images";
        files = Directory.GetFiles(Path.Combine(pathToExport, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Add fonts to the archive
        subfolder = "fonts";
        files = Directory.GetFiles(Path.Combine(pathToExport, subfolder), "*", SearchOption.TopDirectoryOnly);
        zip.AddFiles(files, Path.Combine(baseFolder, subfolder));

        // Save the archive
        FileStream fileStream = new FileStream(ZipFileName, FileMode.OpenOrCreate);
        zip.Save(fileStream);
        fileStream.Close();

        this.GetComponent<Whisp>().Say("HTML export was successfully saved into " + ZipFileName + ".");
    }
}
