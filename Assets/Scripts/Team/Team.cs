using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Team : MonoBehaviour
{
    [SerializeField] private List<Squad> squadsOnTeam = new List<Squad>();
    [SerializeField] private Material teamMaterial;
    [SerializeField] private string teamName;

    public Material TeamMaterial => teamMaterial;
    public string TeamName => teamName;

    public bool AllSquadsReady => squadsOnTeam.Find(squad => !squad.Ready) == null;
    public TeamSelection Selection { get; private set; }
    public TeamOrders Orders { get; private set; }

    private void Awake()
    {
        Selection = new TeamSelection(this);
        Orders = new TeamOrders(this);
        Initiative.AddTeam(this);
        squadsOnTeam.AddRange(GetComponentsInChildren<Squad>());
        squadsOnTeam.RemoveAll(squad => squad == null);
        SquadMovement.OnOrderMove += CheckReactiveFire;
        for (int i = 0; i < squadsOnTeam.Count; i++)
        {
            squadsOnTeam[i].OnSquadInitiativeFailure += GiveUpInitiative;
        }
        StartCoroutine(CommandExecutionCheck());
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
        if (Initiative.TeamWithInitiative != this && !SquadIsOnTeam(movingSquad))
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

    public IEnumerator CommandExecutionCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            Orders.ExecuteCommand();
        }
    }

}
