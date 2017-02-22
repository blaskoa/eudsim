using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Line : MonoBehaviour,IPointerClickHandler
{
    
    public GameObject Begin;
    public GameObject End = null;
    public Vector3 EndPos;
    private LineRenderer _line;
    private Vector3 _startPos;
    private bool _addedCollider = false;
    private Vector3 _oldStartPos;
    private Vector3 _oldEndPos;
    private BoxCollider2D _col1;
    private BoxCollider2D _col2;
    public static GameObject SelectedLine;
    private Vector3 _middlePos;
    public string TypeOfLine = "NoBreak";
    private string _oldTypeOfLine;


    // Update is called once per frame
    void Update()
    {  
        _startPos.x = Begin.transform.position.x;
        _startPos.y = Begin.transform.position.y;
        _startPos.z = Begin.transform.position.z;

        //when i correctly connect two connectors
        if (End != null)
        {
            EndPos.x = End.transform.position.x;
            EndPos.y = End.transform.position.y;
            EndPos.z = End.transform.position.z;

            //when space did not pressed - no break line
            if (TypeOfLine == "NoBreak")
            {
                _line = GetComponent<LineRenderer>();
                _line.SetVertexCount(2);

                _line.SetPosition(0, _startPos);
                _line.SetPosition(1, EndPos);
            }

            else
            {
                //get offset for filling curve
                float offset;
                if (_startPos.x > EndPos.x)
                {
                    offset = 0.015f;
                }
                else
                {
                    offset = -0.015f;
                }

                //when space was 1 times pressed - right break line
                if (TypeOfLine == "RightBreak")
                {
                    _line = GetComponent<LineRenderer>();
                    _line.SetVertexCount(4);

                    _line.SetPosition(0, new Vector3(_startPos.x, _startPos.y, -1));
                    _line.SetPosition(1, new Vector3(EndPos.x + offset, _startPos.y, -1));
                    _line.SetPosition(2, new Vector3(EndPos.x, _startPos.y, -1));
                    _line.SetPosition(3, new Vector3(EndPos.x, EndPos.y, -1));

                    _middlePos = new Vector3(EndPos.x, _startPos.y, -1);
                }

                //when space was 2 times pressed - left break line
                else if (TypeOfLine == "LeftBreak")
                {
                    _line = GetComponent<LineRenderer>();
                    _line.SetVertexCount(4);

                    _line.SetPosition(0, new Vector3(EndPos.x, EndPos.y, -1));
                    _line.SetPosition(1, new Vector3(_startPos.x - offset, EndPos.y, -1));
                    _line.SetPosition(2, new Vector3(_startPos.x, EndPos.y, -1));
                    _line.SetPosition(3, new Vector3(_startPos.x, _startPos.y, -1));

                    _middlePos = new Vector3(_startPos.x, EndPos.y, -1);
                }
            }

            //added dynamic collider in case that moving electric components or changing type of line
            if (!_addedCollider)
            {
                _oldStartPos = _startPos;
                _oldEndPos = EndPos;
                _oldTypeOfLine = TypeOfLine;
                AddCollidersToLine();
                _addedCollider = true;
            }
        }

        //when dragging the line
        else
        {
            _line = GetComponent<LineRenderer>();
            _line.SetVertexCount(2);
            _line.SetPosition(0, _startPos);
            _line.SetPosition(1, EndPos);
        }

        //destroy old colliders in case that moving electric components or changing type of line
        if (_addedCollider && (_oldStartPos != _startPos || _oldEndPos != EndPos || _oldTypeOfLine != TypeOfLine))
        {
            for (int i = 0; i < _line.transform.childCount; i++)
            {
                Destroy(_line.transform.GetChild(i).gameObject);
            }
            _addedCollider = false;
        }   
    }

    private void AddCollidersToLine()
    {
        if (TypeOfLine == "NoBreak")
        {
            _col1 = new GameObject("Collider").AddComponent<BoxCollider2D>();

            // Colliders is added as child object of line
            _col1.transform.parent = _line.transform;

            float lineLegth = Vector2.Distance(_startPos, EndPos);

            // size of collider is set where X is length of line, Y is width of line
            _col1.size = new Vector2(lineLegth, 0.3f);
            Vector3 midPoint = (_startPos + EndPos)/2;

            // setting position of collider object
            _col1.transform.position = midPoint;

            // Following lines calculate the angle between startPos and EndPos
            float angle = (Mathf.Abs(_startPos.y - EndPos.y)/Mathf.Abs(_startPos.x - EndPos.x));

            if ((_startPos.y < EndPos.y && _startPos.x > EndPos.x) || (EndPos.y < _startPos.y && EndPos.x > _startPos.x))
            {
                angle *= -1;
            }

            angle = Mathf.Rad2Deg*Mathf.Atan(angle);
            _col1.transform.Rotate(0, 0, angle);

            //set to ActiveItem
            _col1.gameObject.layer = 8;
        }

        else
        {
            _col1 = new GameObject("Collider").AddComponent<BoxCollider2D>();
            _col2 = new GameObject("Collider").AddComponent<BoxCollider2D>();

            // Colliders is added as child object of line
            _col1.transform.parent = _line.transform;
            _col2.transform.parent = _line.transform;

            //get distance between connectors and break line position
            float lineLegth1 = Vector2.Distance(_startPos, _middlePos);
            float lineLegth2 = Vector2.Distance(_middlePos, EndPos);

            // size of colliders width and length
            if (TypeOfLine == "RightBreak")
            {               
                _col1.size = new Vector2(lineLegth1, 0.3f);
                _col2.size = new Vector2(0.3f, lineLegth2);
            }

            else if (TypeOfLine == "LeftBreak")
            {
                _col1.size = new Vector2(0.3f, lineLegth1);
                _col2.size = new Vector2(lineLegth2, 0.3f);                               
            }

            Vector2 midPoint1 = (_startPos + _middlePos) / 2;
            Vector2 midPoint2 = (_middlePos + EndPos) / 2;

            // setting position of colliders object
            _col1.transform.position = midPoint1;
            _col2.transform.position = midPoint2;

            //set to ActiveItem
            _col1.gameObject.layer = 8;
            _col2.gameObject.layer = 8;
        }
    }

    //select line with mouse click and change color of selected line
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject propertiesContainer = GameObject.Find("PropertiesWindowContainer");
        EditObjectProperties script = propertiesContainer.GetComponent<EditObjectProperties>();

        //deselect component
        if (SelectObject.SelectedObjects.Count != 0)
        {
            foreach (GameObject objectSelected in SelectObject.SelectedObjects)
            {
                objectSelected.transform.FindChild("SelectionBox").GetComponent<SpriteRenderer>().enabled = false;
            }           
            SelectObject.SelectedObjects.Clear();
            script.Clear();
        }

        if (SelectedLine != null)
        {
            SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black, Color.black);
            SelectedLine = this.gameObject;
        }

        SelectedLine = this.gameObject;
        SelectedLine.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
    }
}