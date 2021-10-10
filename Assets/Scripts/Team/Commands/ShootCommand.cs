using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : SquadCommand
{
    private Squad attackTarget;
    private Team squadTeam;

    public ShootCommand(Squad squad, Team squadTeam, Squad attackTarget) : base(squad)
    {
        this.attackTarget = attackTarget;
        this.squadTeam = squadTeam;
    }

    public override void Execute()
    {
        if (squad.Effects.IsSuppressed)
        {
            return;
        }
        if (!squadTeam.SquadIsOnTeam(attackTarget))
        {
            attackTarget.Movement.CentralizePosition();
            squad.Combat.Attack(attackTarget);
        }
    }
}
