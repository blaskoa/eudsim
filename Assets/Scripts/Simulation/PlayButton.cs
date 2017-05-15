using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayButton : MonoBehaviour
{
    public void StopSimulation()
    {
        GameObject playbutton = GameObject.Find("PlayButton");

        playbutton.GetComponent<GUICircuit>().StopSimulation();
   }
}

