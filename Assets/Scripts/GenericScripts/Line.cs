using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Line : MonoBehaviour
{
    
    public GameObject Begin;
    public GameObject End = null;
    public Vector3 EndPos;
    private LineRenderer _line;
    private Vector3 _startPos;
    private bool _addedCollider = false;
    private Vector3 _oldStartPos;
    private Vector3 _oldEndPos;
    private bool _selected;


    // Update is called once per frame
    void Update()
    {  
        _startPos.x = Begin.transform.position.x;
        _startPos.y = Begin.transform.position.y;
        _startPos.z = -1;

        //when i correctly connect two connectors
        if (End != null)
        {
            EndPos.x = End.transform.position.x;
            EndPos.y = End.transform.position.y;
            EndPos.z = -1;
           
        }

        //print line between two positions
        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(2);
        _line.SetPosition(0, _startPos);
        _line.SetPosition(1, EndPos);

        if (End != null && !_addedCollider)
        {
            _oldStartPos = _startPos;
            _oldEndPos = EndPos;
            AddColliderToLine();
            _addedCollider = true;
        }

        if (_addedCollider && (_oldStartPos != _startPos || _oldEndPos != EndPos))
        {
            Destroy(_line.transform.GetChild(0).gameObject);
            _addedCollider = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100) )
            {
                if (!_selected)
                {
                    _line.SetColors(Color.red, Color.red);
                    _selected = true;
                }
                else
                {
                    _line.SetColors(Color.black, Color.black);
                    _selected = false;
                }
            }
        }

    }

    private void AddColliderToLine()
    {
        BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();

        col.transform.parent = _line.transform; // Collider is added as child object of line
        float lineLegth = Vector3.Distance(_startPos, EndPos); // length of line
        col.size = new Vector3(lineLegth, 0.3f, 0.0f); // size of collider is set where X is length of line, Y is width of line
        Vector3 midPoint = (_startPos + EndPos) / 2;
        col.transform.position = midPoint; // setting position of collider object

        // Following lines calculate the angle between startPos and EndPos
        float angle = (Mathf.Abs(_startPos.y - EndPos.y) / Mathf.Abs(_startPos.x - EndPos.x));
        if ((_startPos.y < EndPos.y && _startPos.x > EndPos.x) || (EndPos.y < _startPos.y && EndPos.x > _startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
    }

}