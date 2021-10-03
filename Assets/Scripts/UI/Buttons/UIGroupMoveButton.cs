using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroupMoveButton : UIOrderButton
{
    public override void ClickButton()
    {
        if (!playerTeam.GroupMovement.Active)
        {
            playerTeam.GroupMovement.StartNewGroupMove(targetSquad);
        }
        else
        {
            playerTeam.GroupMovement.ExecuteMove();
        }
    }

    public override void OnHideUI()
    {

    }

    public override void OnShowUI()
    {

    }
}
