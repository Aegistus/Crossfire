using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTargetCommand : SquadCommand
{
    private Squad attackTarget;
    private Team receiverTeam;

    public AttackTargetCommand(Squad receiver, Team receiverTeam, Squad attackTarget) : base(receiver)
    {
        this.attackTarget = attackTarget;
        this.receiverTeam = receiverTeam;
    }

    public override void Execute()
    {
        if (!receiverTeam.SquadIsOnTeam(attackTarget))
        {
            attackTarget.Movement.CentralizePosition();
            receiver.Combat.Attack(attackTarget);
        }
    }
}
