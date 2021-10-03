using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyCommand : SquadCommand
{
    public RallyCommand(Squad receiver) : base(receiver)
    {

    }

    public override void Execute()
    {
        if (receiver.Effects.IsPinned || receiver.Effects.IsSuppressed)
        {
            receiver.Effects.Rally();
        }
    }
}
