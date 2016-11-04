using UnityEngine;
using System.Collections;


public class Line : MonoBehaviour
{

    LineRenderer line;
    Transform tr1;
    Transform tr2;
    public string name1;
    public string name2;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        tr1 = GameObject.Find(name1).transform;
        tr2 = GameObject.Find(name2).transform;
        line = GetComponent<LineRenderer>();
        line.SetVertexCount(2);
        line.SetPosition(0, tr1.transform.position);
        line.SetPosition(1, tr2.transform.position);
	}
}