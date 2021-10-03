using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SquadCommand : ICommand
{
    public Squad Squad { get => squad; }
    protected Squad squad;

    public SquadCommand(Squad squad)
    {
        this.squad = squad;
    }

    public abstract void Execute();
}
