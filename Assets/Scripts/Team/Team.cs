using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Team : MonoBehaviour
{
    [SerializeField]
    private List<Squad> squadsOnTeam = new List<Squad>();

    public bool ReadyForOrders => squadsOnTeam.Find(squad => !squad.Ready) == null;
    private bool executeOrders = true;

    public TeamSelection Selection { get; private set; }

    private Queue<SquadCommand> commandQueue = new Queue<SquadCommand>();

    private void Awake()
    {
        Selection = new TeamSelection(this);
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

    public void StartGroupMovement()
    {
        executeOrders = false;
    }

    public void ExecuteGroupMovement()
    {
        executeOrders = true;
        StartCoroutine(OrderCoroutine());
    }

    public void AddNewCommand(SquadCommand command)
    {
        commandQueue.Enqueue(command);
    }

    private IEnumerator OrderCoroutine()
    {
        while (commandQueue.Count > 0 && executeOrders)
        {
            if (Initiative.TeamWithInitiative != this)
            {
                Debug.Log("You Do Not Have Initiative");
                commandQueue.Clear();
            }
            if (!ReadyForOrders)
            {
                yield return null;
            }
            else
            {
                commandQueue.Dequeue().Execute();
            }
        }
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
