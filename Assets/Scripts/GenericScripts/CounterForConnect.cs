using UnityEngine;
using System.Collections;

//class for computing helpful informations for drawing line between connectors 
public class CounterForConnect {

    private static int _count = 0;
    private static GameObject _previous;

    public int GetCount()
    {
        return _count;
    }

    public void Increment()
    {
        _count++;
    }

    public void Decrement()
    {
        _count--;
    }

    public void ResetCount()
    {
        _count = 0;
    }

    public void SetPrevious(GameObject prev) {
        _previous = prev;
    }

    public GameObject GetPrevious()
    {
        return _previous;
    }
}
