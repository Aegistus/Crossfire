using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : SquadCommand
{
    private Vector3 position;

    public MoveCommand(Squad receiver, Vector3 position) : base(receiver)
    {
        this.position = position;
        receiver.Movement.CalculatePath(position);
    }

    public override void Execute()
    {
        if (squad.Cover == CoverType.HalfCover || squad.Cover == CoverType.FullCover)
        {
            squad.Movement.MoveOutOfCover();
        }
        squad.Movement.Move(position);
    }
}
