using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{
    
    LineRenderer _line;
    public GameObject Begin;
    public GameObject End;
    public Vector3 _startPos;
    public Vector3 _endPos;
    private Vector3 _screenPoint; 

    void Start()
    {
       // _screenPoint = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //setting positions (x,y,z) of two connected objects with line
        //_startPos.y = Begin.transform.parent.gameObject.transform.position.y;
        //_endPos.y = End.transform.parent.gameObject.transform.position.y;
        _startPos.x = Begin.transform.position.x;
        _startPos.y = Begin.transform.position.y;
        _endPos.x = End.transform.position.x;
        _endPos.y = End.transform.position.y;
        _startPos.z = -1;
        _endPos.z = -1;

       /*
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
        }*/

        /*Vector3 curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        curPosition.z = 0;*/


        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(2);
        _line.SetPosition(0, _startPos);
        _line.SetPosition(1, _endPos);


        /*GameObject[] objs = GameObject.FindGameObjectsWithTag("connector");
        GameObject myObject = null;
        curPosition *= 2;
        curPosition = new Vector3(Mathf.Round(curPosition.x), Mathf.Round(curPosition.y));
        curPosition /= 2;
        foreach (GameObject go in objs)
        {
            go.transform.position *= 2;
            go.transform.position = new Vector3(Mathf.Round(go.transform.position.x), Mathf.Round(go.transform.position.y));
            go.transform.position /= 2;
           
            if (go.transform.position == curPosition)
            {
                Debug.Log(go.name + " " + go.transform.position + " " + curPosition);
                myObject = go;
                break;
                
            }
            
        }*/

    }
}