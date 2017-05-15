using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scrollbars : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private float _maxValue = 1f;
    private float _minValue = 0f;
    private bool _isListening = false;

    void OnGUI()
    {
        if (_isListening)
        {          
            GameObject scrollbar = this.gameObject.transform.FindChild("Scrollbar").gameObject;
            float value = scrollbar.GetComponent<Scrollbar>().value;

            // Scrolling up.
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                // Checking for max zoom in.
                if  (value < _maxValue)
                {
                    scrollbar.GetComponent<Scrollbar>().value += 0.01f;
                }
            }
            // Scrolling down.
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                // Checking for max zoom out.
                if (value > _minValue)
                {
                    scrollbar.GetComponent<Scrollbar>().value -= 0.01f;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isListening = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isListening = false;
    }
}
