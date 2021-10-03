using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyCommand : SquadCommand
{
    public RallyCommand(Squad squad) : base(squad)
    {

    }

    public override void Execute()
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            squad.Effects.Rally();
        }
    }
}
