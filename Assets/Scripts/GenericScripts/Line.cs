using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{
    
    public GameObject Begin;
    public GameObject End = null;
    public Vector3 EndPos;


    // Update is called once per frame
    void Update()
    {
        Vector3 startPos;
        startPos.x = Begin.transform.position.x;
        startPos.y = Begin.transform.position.y;
        startPos.z = -1;

        //when i correctly connect two connectors
        if (End != null)
        {
            EndPos.x = End.transform.position.x;
            EndPos.y = End.transform.position.y;
            EndPos.z = -1;
        }

        //print line between two positions
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetPosition(0, startPos);
        line.SetPosition(1, EndPos);

    }
}