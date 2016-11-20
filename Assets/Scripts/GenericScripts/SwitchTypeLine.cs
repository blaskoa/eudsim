using UnityEngine;

public class SwitchTypeLine : MonoBehaviour {

    private float _buttonDelay;
    private float _delay = 0.25f;

    // Update is called once per frame
    void Update ()
    {
        DecreaseDelay();

        if (Line.SelectedLine != null && this.gameObject == Line.SelectedLine)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (this.gameObject.GetComponent<Line>()._typeOfLine == 3)
                {
                    this.gameObject.GetComponent<Line>()._typeOfLine = 0;
                }

                // Button delay has passed and some keys were pressed.
                if (_buttonDelay == 0f)
                {
                    this.gameObject.GetComponent<Line>()._typeOfLine++;
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
