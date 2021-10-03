using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPointCommand : SquadCommand
{
    private Vector3 position;

    public MoveToPointCommand(Squad receiver, Vector3 position) : base(receiver)
    {
        this.position = position;
    }

    public override void Execute()
    {
        if (receiver.Cover == CoverType.HalfCover || receiver.Cover == CoverType.FullCover)
        {
            receiver.Movement.MoveOutOfCover();
        }
        receiver.Movement.Move(position);
    }
}
