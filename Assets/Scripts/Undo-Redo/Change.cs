using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Change : MonoBehaviour
{
    public bool CreateDestroyType;

    public Change()
    {
        CreateDestroyType = false;
    }

    public virtual void SetChange(List<float> properties)
    {

    }

    public virtual void UndoChange()
    {

    }

    public virtual void RedoChange()
    {

    }
}
