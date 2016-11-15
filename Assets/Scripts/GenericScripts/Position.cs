using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour
{

    private Vector3 a;

	// Use this for initialization
	void Start ()
	{
	     a = Input.mousePosition;

	}
	
	// Update is called once per frame
	void Update () {
        
        Debug.Log(a.x + " " + a.y + " " + a.z);
	}
}
