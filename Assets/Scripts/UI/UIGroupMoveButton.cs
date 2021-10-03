using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroupMoveButton : UIOrderButton
{

    public override void ClickButton()
    {
        if (targetSquad != null)
        {
            playerTeam.Orders.AddCommandToQueue(new GroupMoveCommand(targetSquad, playerTeam));
        }
    }

    public override void OnHideUI()
    {

    }

    public override void OnShowUI()
    {

    }
}
