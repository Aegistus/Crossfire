using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Initiative
{
    public static event Action<Team> OnInitiativeChange;

    public static Team TeamWithInitiative { get; private set; }

    private static List<Team> allTeams = new List<Team>();
    private static Queue<Team> teamQueue;
    private static bool gameStarted = false;

    public static void StartGame()
    {
        if (!gameStarted)
        {
            teamQueue = new Queue<Team>(allTeams);
            for (int i = 0; i < allTeams.Count; i++)
            {
                Team team = GetNextTeam();
                if (team is PlayerTeam)
                {
                    TeamWithInitiative = team;
                    break;
                }
            }
            if (TeamWithInitiative == null)
            {
                TeamWithInitiative = GetNextTeam();
            }
            gameStarted = true;
            OnInitiativeChange?.Invoke(TeamWithInitiative);
        }
    }

    private static Team GetNextTeam()
    {
        Team team = teamQueue.Dequeue();
        teamQueue.Enqueue(team);
        return team;
    }

    public static void AddTeam(Team team)
    {
        allTeams.Add(team);
    }

    public static void PassInitiative()
    {
        Debug.Log("Passing Initiative");
        TeamWithInitiative = GetNextTeam();
        OnInitiativeChange?.Invoke(TeamWithInitiative);
    }
}
