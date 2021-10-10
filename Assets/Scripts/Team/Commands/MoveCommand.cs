using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : SquadCommand
{
    private Vector3 position;

    public MoveCommand(Squad receiver, Vector3 position) : base(receiver)
    {
        this.position = position;
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        receiver.Movement.CalculatePath(position);
    }

    public override void Execute()
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        if (squad.Cover == CoverType.HalfCover || squad.Cover == CoverType.FullCover)
        {
            squad.Movement.LeaveCover();
        }
        squad.Movement.Move(position);
    }
}
