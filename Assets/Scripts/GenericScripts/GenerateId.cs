using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class GenerateId : MonoBehaviour 
{
    // Id's for each component in Prefabs
    public int[] generatedIds;
    
    void Start()
    {
        generatedIds = new int[20];
        foreach (int i in generatedIds){
            generatedIds[i] = 0;
        }
    }
}