using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SquadCommand
{
    protected Squad receiver;

    public SquadCommand(Squad receiver)
    {
        this.receiver = receiver;
    }

    public abstract void Execute();
}
