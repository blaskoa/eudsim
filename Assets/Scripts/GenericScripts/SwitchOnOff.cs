using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ClassLibrarySharpCircuit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class SwitchOnOff : MonoBehaviour, IPointerClickHandler {

    private Sprite _switchOff;
    private Sprite _switchOn;
   

    void Start()
    {
         _switchOff = Resources.Load<Sprite>("switch");
         _switchOn = Resources.Load<Sprite>("switch2");       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //changing image sprite of switch object
        if (SelectObject.SelectedObjects.Count == 1 && SelectObject.SelectedObjects[0] == this.gameObject)
        {
            Image image = this.gameObject.GetComponent<Image>();
            SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

            GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
            Toggle checkBox = propertiesContainer.transform.FindChild("TurnedOffPropertyLabel").gameObject.GetComponent<Toggle>();
            

            if (image.sprite.name == _switchOff.name)
            {
                image.sprite = _switchOn;
                spriteRenderer.sprite = _switchOn;
                checkBox.isOn = true;
            }
            else if (image.sprite.name == _switchOn.name)
            {
                image.sprite = _switchOff;
                spriteRenderer.sprite = _switchOff;
                checkBox.isOn = false;
            }

        }
    }

}
