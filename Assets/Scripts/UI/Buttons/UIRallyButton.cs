using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRallyButton : UIOrderButton
{
    public override void ClickButton()
    {
        if (playerTeam != null && targetSquad != null)
        {
            playerTeam.Orders.AddCommandToQueue(new RallyCommand(targetSquad));
        }
    }

    public override void OnHideUI()
    {
        if (targetSquad != null)
        {
            targetSquad.Effects.OnEffectChange -= CheckSquadEffects;
        }
    }

    public override void OnShowUI()
    {
        if (targetSquad != null)
        {
            CheckSquadEffects();
            targetSquad.Effects.OnEffectChange += CheckSquadEffects;
        }
    }

    private void CheckSquadEffects()
    {
        if (targetSquad.Effects.IsPinned || targetSquad.Effects.IsSuppressed)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
}
