using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Manual : MonoBehaviour
{
    private string _manualPath = "manual/index.html";

    // Calls system to open the HTML manual
    public void openManual()
    {
        Application.OpenURL(_manualPath);
    }
}
