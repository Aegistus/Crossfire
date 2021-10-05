using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMoveCommand : SquadCommand
{
    private Team squadTeam;

    public GroupMoveCommand(Squad squad, Team squadTeam) : base(squad)
    {
        this.squadTeam = squadTeam;
    }

    public override void Execute()
    {
        if (squadTeam.Orders.ExecuteOrders)
        {
            squadTeam.Orders.PauseCommandExecution();
        }
    }
}
