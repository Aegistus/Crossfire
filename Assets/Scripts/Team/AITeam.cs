using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITeam : Team
{
    private void Start()
    {
        Initiative.OnInitiativeChange += AutoGiveUpInitiative;
    }

    // for testing purposes
    private void AutoGiveUpInitiative(Team team)
    {
        if (team == this)
        {
            GiveUpInitiative();
        }
    }
}
