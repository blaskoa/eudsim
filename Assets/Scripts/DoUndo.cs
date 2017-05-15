﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


class DoUndo : MonoBehaviour
{
    public void PerformUndo()
    {
        GUICircuitComponent.globalUndoList.Undo();
    }
    public void PerformRedo()
    {
        GUICircuitComponent.globalUndoList.Redo();
    }
}