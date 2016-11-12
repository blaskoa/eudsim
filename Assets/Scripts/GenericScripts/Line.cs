using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{
    
    LineRenderer _line;
    public GameObject Begin;
    public GameObject End;
    private Vector3 _startPos;
    private Vector3 _endPos;

    // Update is called once per frame
    void Update()
    {
        //setting positions (x,y,z) of two connected objects with line
        _startPos.y = Begin.transform.parent.gameObject.transform.position.y;
        _endPos.y = End.transform.parent.gameObject.transform.position.y;
        _startPos.z = -1;
        _endPos.z = -1;

        //search concrete connector of components (left, right) to set position 
        if (Begin.transform.parent.gameObject.transform.position.x - Begin.transform.position.x < 0)
        {
            _startPos.x = Begin.transform.parent.gameObject.transform.position.x + 0.4f;
        }
        else if (Begin.transform.parent.gameObject.transform.position.x - Begin.transform.position.x > 0)
        {
            _startPos.x = Begin.transform.parent.gameObject.transform.position.x - 0.4f;
        }

        if (End.transform.parent.gameObject.transform.position.x - End.transform.position.x < 0)
        {
            _endPos.x = End.transform.parent.gameObject.transform.position.x + 0.4f;
        }
        else if (End.transform.parent.gameObject.transform.position.x - End.transform.position.x > 0)
        {
            _endPos.x = End.transform.parent.gameObject.transform.position.x - 0.4f;
        }

        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(2);
        _line.SetPosition(0, _startPos);
        _line.SetPosition(1, _endPos);
    }
}