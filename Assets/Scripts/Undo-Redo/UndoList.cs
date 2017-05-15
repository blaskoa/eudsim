using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UndoList
{
    public LinkedList<UndoAction> undoList = new LinkedList<UndoAction>();
    public LinkedList<UndoAction> redoList = new LinkedList<UndoAction>();

    public void AddUndo(UndoAction undoAction)
    {
        if(undoList.Count == 5)
        {
            undoList.RemoveLast();
        }
    
        FlushRedoActions();
        undoList.AddFirst(undoAction);
    }

    public void Undo()
    {
        if (undoList.Count != 0)
        {
            undoList.First().UndoChanges();
            redoList.AddFirst(undoList.First());
            undoList.RemoveFirst();
        }
    }

    public void Redo()
    {
        if (redoList.Count != 0)
        {
            redoList.First().RedoChanges();
            undoList.AddFirst(redoList.First());
            redoList.RemoveFirst();
        }
    }

    public void FlushRedoActions()
    {
        redoList.Clear();
    }
}
