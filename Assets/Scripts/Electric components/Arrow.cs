using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Arrow : MonoBehaviour
{

    private GameObject _potentiometer;
    private GUIPotentiometer _script;

    void Start()
    {
        _potentiometer = this.gameObject.transform.parent.gameObject;
        _script = _potentiometer.GetComponent<GUIPotentiometer>();
    }


	// Update is called once per frame
	void Update ()
	{
	    float diff = this.gameObject.transform.position.x - _potentiometer.transform.position.x;
	    //_script.Position = diff;
	}
}
