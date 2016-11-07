using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{

    LineRenderer line;
    public GameObject start;
    public GameObject end;
    private Vector3 startPos;
    private Vector3 endPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        startPos.y = start.transform.parent.gameObject.transform.position.y;
        endPos.y = end.transform.parent.gameObject.transform.position.y;
        startPos.z = -1;
        endPos.z = -1;

        if (start.transform.parent.gameObject.transform.position.x - start.transform.position.x < 0)
        {
            startPos.x = start.transform.parent.gameObject.transform.position.x + 0.4f;
        }
        else if (start.transform.parent.gameObject.transform.position.x - start.transform.position.x > 0)
        {
            startPos.x = start.transform.parent.gameObject.transform.position.x - 0.4f;
        }

        if (end.transform.parent.gameObject.transform.position.x - end.transform.position.x < 0)
        {
            endPos.x = end.transform.parent.gameObject.transform.position.x + 0.4f;
        }
        else if (end.transform.parent.gameObject.transform.position.x - end.transform.position.x > 0)
        {
            endPos.x = end.transform.parent.gameObject.transform.position.x - 0.4f;
        }

        line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
}