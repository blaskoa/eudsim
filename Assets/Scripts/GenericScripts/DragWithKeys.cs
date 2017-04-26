using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Hotkeys;

public class DragWithKeys : MonoBehaviour {

    // hotkey keys
    public const string MoveDownHotkeyKey = "MoveDown";
    public const string MoveUpHotkeyKey = "MoveUp";
    public const string MoveLeftHotkeyKey = "MoveLeft";
    public const string MoveRightHotkeyKey = "MoveRight";

    private float _step = 0.5f;
    private float _buttonDelay = 0f;
    private float _delay = 0.25f;
	
	// Update is called once per frame
	void Update () {

        // Vector for movement.
        Vector3 movement = new Vector3();

        _decreaseDelay();

        // Check if A is pressed.
        if (HotkeyManager.Instance.CheckHotkey(MoveLeftHotkeyKey))
        {
            if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'A')
            {
                CollisionUtils.Move = true;
            }
            CollisionUtils.LastPressed = 'A';
            movement.x -= _step;
        }

        // Check if D is pressed.
        if (HotkeyManager.Instance.CheckHotkey(MoveRightHotkeyKey))
        {
            if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'D')
            {
                CollisionUtils.Move = true;
            }
            CollisionUtils.LastPressed = 'D';
            movement.x += _step;
        }

        // Check if S is pressed.
        if (HotkeyManager.Instance.CheckHotkey(MoveDownHotkeyKey))
        {
            if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'S')
            {
                CollisionUtils.Move = true;
            }
            CollisionUtils.LastPressed = 'S';
            movement.y -= _step;
        }

        // Check if W is pressed.
        if (HotkeyManager.Instance.CheckHotkey(MoveUpHotkeyKey))
        {
            if (CollisionUtils.Move == false && CollisionUtils.LastPressed != 'W')
            {
                CollisionUtils.Move = true;
            }
            CollisionUtils.LastPressed = 'W';
            movement.y += _step;
        }

        // Button delay has passed and some keys were pressed. 
        if (_buttonDelay == 0f && (movement.x != 0f || movement.y != 0f))
        {
            // Check if any object is selected.
            if (SelectObject.SelectedObjects.Count != 0)
            {
                GameObject[] gameObjects1 = GameObject.FindGameObjectsWithTag("ActiveItem");
                GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("ActiveNode");

                //merge two arrays to one
                GameObject[] gameObjects = gameObjects1.Concat(gameObjects2).ToArray();

                foreach (GameObject objectSelected in SelectObject.SelectedObjects)
                {
                    //check collision with every active item
                    foreach (GameObject go in gameObjects)
                    {
                        if (go != objectSelected)
                        {
                            if (Math.Abs((go.transform.position.x + objectSelected.transform.position.x + movement.x)/
                                            2 - go.transform.position.x) <= 0.3 &&
                                Math.Abs((go.transform.position.y + objectSelected.transform.position.y + movement.y)/
                                            2 - go.transform.position.y) <= 0.3)
                            {
                                CollisionUtils.Move = false;
                            }
                        }
                    }
                }

                foreach (GameObject objectSelected in SelectObject.SelectedObjects)
                {
                    //if is possible to move
                    if (CollisionUtils.Move)
                    {
                        objectSelected.transform.position = objectSelected.transform.position + movement;
                    }
                    _buttonDelay = _delay;

                    //transform position of each lines in scene
                    Line.TransformLines();
                }
            }
        }
    }

    // To limit users's spamming and make grid-like movement.
    private void _decreaseDelay()
    {
        if (_buttonDelay > 0f)
        {
            _buttonDelay -= Time.deltaTime;
        }
        if (_buttonDelay < 0f)
        {
            _buttonDelay = 0f;
        }
    }
}
