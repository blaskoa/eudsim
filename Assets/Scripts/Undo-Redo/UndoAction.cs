using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UndoAction
{
    public List<Change> changes = new List<Change>();

    public void AddChange(Change change)
    {
        changes.Add(change);
    }

    public void UndoChanges()
    {
        for (int i = 0; i < changes.Count; i++)     // tunak dat ze ak je to createchange,, tak 2 krat po sebe dat tie fory.. potom v jednom suc a v druhom spojenia,, nastavit priznaky kedy co
        {
            changes[i].UndoChange();
        }

        if (changes[0].CreateDestroyType == true)
        {
            for (int i = 0; i < changes.Count; i++)
            {
                changes[i].UndoChange();
            }
        }
    }

    public void RedoChanges()
    {
        for (int i = changes.Count-1; i >= 0; i--)
        {
            changes[i].RedoChange();
        }

        if (changes[0].CreateDestroyType == true)
        {
            for (int i = 0; i < changes.Count; i++)
            {
                changes[i].RedoChange();
            }
        }
    }
}
