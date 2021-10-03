using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : SquadCommand
{
    private Squad attackTarget;
    private Team receiverTeam;

    public ShootCommand(Squad receiver, Team receiverTeam, Squad attackTarget) : base(receiver)
    {
        this.attackTarget = attackTarget;
        this.receiverTeam = receiverTeam;
    }

    public override void Execute()
    {
        if (!receiverTeam.SquadIsOnTeam(attackTarget))
        {
            attackTarget.Movement.CentralizePosition();
            squad.Combat.Attack(attackTarget);
        }
    }
}
