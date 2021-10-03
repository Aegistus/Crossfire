using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamOrders
{
    public static event Action<Squad> OnOrderMove;

    public bool ExecuteOrders { get; private set; } = true;
    public bool HasCommands => commandQueue.Count > 0;

    private Queue<SquadCommand> commandQueue = new Queue<SquadCommand>();
    private Team team;

    public TeamOrders(Team team)
    {
        this.team = team;
    }

    public void PauseCommandExecution()
    {
        ExecuteOrders = false;
    }

    public void ResumeCommandExecution()
    {
        ExecuteOrders = true;
    }

    public void AddCommandToQueue(SquadCommand command)
    {
        commandQueue.Enqueue(command);
    }

    public void ExecuteCommand()
    {
        if (!team.AllSquadsReady || !ExecuteOrders || !HasCommands)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have the Initiative");
            commandQueue.Clear();
            return;
        }
        SquadCommand command = commandQueue.Dequeue();
        command.Execute();
        if (command.GetType() == typeof(MoveCommand) || command.GetType() == typeof(CoverMoveCommand))
        {
            OnOrderMove(command.Squad);
        }
    }
}
