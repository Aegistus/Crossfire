using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverMoveCommand : SquadCommand
{
    private Cover cover;

    public CoverMoveCommand(Squad receiver, Cover cover) : base(receiver)
    {
        this.cover = cover;
        receiver.Movement.CalculatePath(cover.transform.position);
    }

    public override void Execute()
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        if (cover.UnOccupied)
        {
            if (squad.Cover == CoverType.HalfCover || squad.Cover == CoverType.FullCover)
            {
                squad.Movement.LeaveCover();
            }
            squad.Movement.MoveToCover(cover);
        }
    }
}
