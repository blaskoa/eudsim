using UnityEngine;
using System.Collections;

public class CounterForConnect {

    public static int count = 0;
    public static GameObject previous;

    public int getCount()
    {
        return count;
    }

    public void increment()
    {
        count++;
    }

    public void decrement()
    {
        count--;
    }

    public void resetCount()
    {
        count = 0;
    }

    public void setPrevious(GameObject prev) {
        previous = prev;
    }

    public GameObject getPrevious()
    {
        return previous;
    }
}
