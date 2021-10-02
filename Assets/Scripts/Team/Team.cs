using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Team : MonoBehaviour
{
    [SerializeField]
    private List<Squad> squadsOnTeam = new List<Squad>();

    public bool ReadyForOrders => squadsOnTeam.Find(squad => !squad.Ready) == null;

    public TeamSelection Selection { get; private set; }
    public TeamOrders Orders { get; private set; }
    public GroupMove GroupMovement { get; private set; }

    private void Awake()
    {
        Selection = new TeamSelection(this);
        Orders = new TeamOrders(this);
        GroupMovement = new GroupMove();
        Initiative.AddTeam(this);
        squadsOnTeam.AddRange(GetComponentsInChildren<Squad>());
        squadsOnTeam.RemoveAll(squad => squad == null);
        TeamOrders.OnOrderMove += CheckReactiveFire;
        for (int i = 0; i < squadsOnTeam.Count; i++)
        {
            squadsOnTeam[i].OnSquadInitiativeFailure += GiveUpInitiative;
        }
    }

    public bool SquadIsOnTeam(Squad squad)
    {
        return squadsOnTeam.Contains(squad);
    }

    protected void GiveUpInitiative()
    {
        if (Initiative.TeamWithInitiative == this)
        {
            Initiative.PassInitiative();
        }
    }

    private void CheckReactiveFire(Squad movingSquad)
    {
        if (Initiative.TeamWithInitiative != this)
        {
            for (int i = 0; i < squadsOnTeam.Count; i++)
            {
                if (squadsOnTeam[i].isActiveAndEnabled)
                {
                    squadsOnTeam[i].Combat.UpdateMovingSquads(movingSquad);
                }
            }
        }
    }

}
