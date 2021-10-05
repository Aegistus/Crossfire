using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroupMoveButton : UIOrderButton
{

    public override void ClickButton()
    {
        print("Test 01");
        if (playerTeam.Orders.ExecuteOrders)
        {
            playerTeam.Orders.AddCommandToQueue(new GroupMoveCommand(targetSquad, playerTeam));
        }
        else
        {
            playerTeam.Orders.ResumeCommandExecution();
        }
    }

    public override void OnHideUI()
    {

    }

    public override void OnShowUI()
    {

    }
}
