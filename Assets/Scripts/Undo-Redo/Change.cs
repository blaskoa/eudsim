using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Change : MonoBehaviour
{
    public bool CreateDestroyType;
    public Vector3 position = new Vector3();

    public Change()
    {
        CreateDestroyType = false;
    }

    public abstract void SetChange(List<float> properties);
    public abstract void UndoChange();
    public abstract void RedoChange();
}
