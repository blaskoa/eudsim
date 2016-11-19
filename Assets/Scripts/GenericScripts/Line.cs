using UnityEngine;

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
    private BoxCollider2D _col;
    public static GameObject SelectedLine;


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

        CheckSelect();     

    }

    //select line with mouse click and change color of selected line
    private void CheckSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);

            if (hitInfo.collider.gameObject.GetComponent<BoxCollider2D>() == _col)
            {
                if (SelectedLine != null)
                {
                    SelectedLine.GetComponent<LineRenderer>().SetColors(Color.black,Color.black);
                    SelectedLine = this.gameObject;
                }

                SelectedLine = this.gameObject;
                SelectedLine.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
            }
        }
    }

    private void AddColliderToLine()
    {
        _col = new GameObject("Collider").AddComponent<BoxCollider2D>();

        // Collider is added as child object of line
        _col.transform.parent = _line.transform; 
        float lineLegth = Vector2.Distance(_startPos, EndPos);

        // size of collider is set where X is length of line, Y is width of line
        _col.size = new Vector2(lineLegth, 0.3f); 
        Vector2 midPoint = (_startPos + EndPos) / 2;

        // setting position of collider object
        _col.transform.position = midPoint; 

        // Following lines calculate the angle between startPos and EndPos
        float angle = (Mathf.Abs(_startPos.y - EndPos.y) / Mathf.Abs(_startPos.x - EndPos.x));

        if ((_startPos.y < EndPos.y && _startPos.x > EndPos.x) || (EndPos.y < _startPos.y && EndPos.x > _startPos.x))
        {
            angle *= -1;
        }

        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        _col.transform.Rotate(0, 0, angle);
    }

}