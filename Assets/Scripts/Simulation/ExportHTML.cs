using System;
using System.Collections;
using System.Collections.Generic;
using ClassLibrarySharpCircuit;
using UnityEngine;
using System.IO;
using System.IO.Compression;
public class ExportHTML : MonoBehaviour
{
    public Camera Camera;
    public void MakeHTMLExport()
    {
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
        string text = File.ReadAllText("ExportHTML/js/edusim-pattern.js");
        string insertPoint = "const hotspots = [";
        int index = text.IndexOf(insertPoint, StringComparison.Ordinal) + insertPoint.Length;
        text = text.Insert(index, string.Join("", exportArrayList.ToArray()));
        File.WriteAllText("ExportHTML/js/edusim.js", text);
        
        this.GetComponent<Whisp>().Say("HTML export was generated into ExportHTML dir.");
    }
}
