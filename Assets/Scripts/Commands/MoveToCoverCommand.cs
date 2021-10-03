using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCoverCommand : SquadCommand
{
    private Cover cover;

    public MoveToCoverCommand(Squad receiver, Cover cover) : base(receiver)
    {
        this.cover = cover;
    }

    public override void Execute()
    {
        if (cover.UnOccupied)
        {
            if (receiver.Cover == CoverType.HalfCover || receiver.Cover == CoverType.FullCover)
            {
                receiver.Movement.MoveOutOfCover();
            }
            receiver.Movement.MoveToCover(cover);
        }
    }
}
