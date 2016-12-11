using Assets.Scripts.Hotkeys;
using UnityEngine;

public class SwitchTypeLine : MonoBehaviour
{
    public const string ChangeLineBrakingHotkeyKey = "LineBreak";
    private float _buttonDelay;
    private float _delay = 0.25f;

    // Update is called once per frame
    void Update ()
    {
        DecreaseDelay();

        if (Line.SelectedLine != null && this.gameObject == Line.SelectedLine)
        {
            if (HotkeyManager.Instance.CheckHotkey(ChangeLineBrakingHotkeyKey))
            {
                // Button delay has passed and some keys were pressed.
                if (_buttonDelay == 0f)
                {
                    if (this.gameObject.GetComponent<Line>().TypeOfLine == "NoBreak")
                    {
                        this.gameObject.GetComponent<Line>().TypeOfLine = "RightBreak";

                    }

                    else if (this.gameObject.GetComponent<Line>().TypeOfLine == "RightBreak")
                    {
                        this.gameObject.GetComponent<Line>().TypeOfLine = "LeftBreak";
                    }

                    else if (this.gameObject.GetComponent<Line>().TypeOfLine == "LeftBreak")
                    {
                        this.gameObject.GetComponent<Line>().TypeOfLine = "NoBreak";
                    }

                    _buttonDelay = _delay;
                }
            }
        }
    }

    private void DecreaseDelay()
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
