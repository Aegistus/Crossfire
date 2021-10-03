using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamOrders
{
    public static event Action<Squad> OnOrderMove;

    public bool GroupEvent { get; private set; }

    private Team team;
    private TeamSelection Selection => team.Selection;

    public TeamOrders(Team team)
    {
        this.team = team;
    }

    public void GiveMoveOrder(Vector3 position)
    {
        Debug.Log("Test");
        if (!team.ReadyForOrders)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have Initiative");
            return;
        }
        for (int i = 0; i < Selection.SelectedUnits.Count; i++)
        {
            if (Selection.SelectedUnits[i].Cover == CoverType.HalfCover || Selection.SelectedUnits[i].Cover == CoverType.FullCover)
            {
                Selection.SelectedUnits[i].Movement.MoveOutOfCover();
            }
            Selection.SelectedUnits[i].Movement.Move(position);
        }
        if (Selection.SelectedUnits.Count > 0)
        {
            OnOrderMove?.Invoke(Selection.SelectedUnits[0]);
        }
    }

    public void GiveMoveToCoverOrder(Cover cover)
    {
        if (!team.ReadyForOrders)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have Initiative");
            return;
        }
        if (cover.UnOccupied)
        {
            for (int i = 0; i < Selection.SelectedUnits.Count; i++)
            {
                if (Selection.SelectedUnits[i].Cover == CoverType.HalfCover || Selection.SelectedUnits[i].Cover == CoverType.FullCover)
                {
                    Selection.SelectedUnits[i].Movement.MoveOutOfCover();
                }
                Selection.SelectedUnits[i].Movement.MoveToCover(cover);
            }
        }
        OnOrderMove?.Invoke(Selection.SelectedUnits[0]);
    }

    public void GiveAttackOrderOnTarget(Squad enemySquad)
    {
        if (!team.ReadyForOrders)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have Initiative");
            return;
        }
        if (!team.SquadIsOnTeam(enemySquad))
        {
            enemySquad.Movement.CentralizePosition();
            for (int i = 0; i < Selection.SelectedUnits.Count; i++)
            {
                Selection.SelectedUnits[i].Combat.Attack(enemySquad);
            }
        }
    }

    public void GiveRallyOrder(Squad targetSquad)
    {
        if (!team.ReadyForOrders)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have Initiative");
            return;
        }
        if (targetSquad.Effects.IsPinned || targetSquad.Effects.IsSuppressed)
        {
            targetSquad.Effects.Rally();
        }
    }
}
