using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverMoveCommand : SquadCommand
{
    private Cover cover;

    public CoverMoveCommand(Squad receiver, Cover cover) : base(receiver)
    {
        this.cover = cover;
    }

    public override void Execute()
    {
        if (cover.UnOccupied)
        {
            if (squad.Cover == CoverType.HalfCover || squad.Cover == CoverType.FullCover)
            {
                squad.Movement.MoveOutOfCover();
            }
            squad.Movement.MoveToCover(cover);
        }
    }
}
