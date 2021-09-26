using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Team : MonoBehaviour
{
    public List<Squad> unitsOnTeam = new List<Squad>();

    public static event Action<Squad> OnOrderMove;

    public bool HasUnitsSelected => selectedUnits.Count > 0;

    private List<Squad> selectedUnits = new List<Squad>();

    private void Awake()
    {
        Initiative.AddTeam(this);
        OnOrderMove += CheckReactiveFire;
        for (int i = 0; i < unitsOnTeam.Count; i++)
        {
            unitsOnTeam[i].OnSquadInitiativeFailure += GiveUpInitiative;
        }
    }

    private void GiveUpInitiative()
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
            print("Test 1");
            for (int i = 0; i < unitsOnTeam.Count; i++)
            {
                unitsOnTeam[i].Combat.UpdateMovingSquads(movingSquad);
            }
        }
    }

    public void SelectSquad(Squad toSelect)
    {
        if (Initiative.TeamWithInitiative != this)
        {
            print("You Do Not Have Initiative");
            return;
        }
        if (unitsOnTeam.Contains(toSelect))
        {
            toSelect.Selection.Select();
            selectedUnits.Add(toSelect);
        }
        else
        {
            print("Unit Not on Team");
        }
    }

    public void DeselectSquad(Squad toDeselect)
    {
        toDeselect.Selection.Deselect();
        selectedUnits.Remove(toDeselect);
    }

    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].Selection.Deselect();
        }
        selectedUnits.Clear();
    }

    public void GiveMoveOrder(Vector3 position)
    {
        if (Initiative.TeamWithInitiative != this)
        {
            print("You Do Not Have Initiative");
            return;
        }
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (selectedUnits[i].Cover == CoverType.HalfCover || selectedUnits[i].Cover == CoverType.FullCover)
            {
                selectedUnits[i].Movement.MoveOutOfCover();
            }
            selectedUnits[i].Movement.Move(position);
        }
        OnOrderMove?.Invoke(selectedUnits[0]);
    }

    public void GiveMoveToCoverOrder(Cover cover)
    {
        if (Initiative.TeamWithInitiative != this)
        {
            print("You Do Not Have Initiative");
            return;
        }
        if (cover.UnOccupied)
        {
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                if (selectedUnits[i].Cover == CoverType.HalfCover || selectedUnits[i].Cover == CoverType.FullCover)
                {
                    selectedUnits[i].Movement.MoveOutOfCover();
                }
                selectedUnits[i].Movement.MoveToCover(cover);
            }
        }
        OnOrderMove?.Invoke(selectedUnits[0]);
    }

    public void GiveAttackOrderOnTarget(Squad enemySquad)
    {
        if (Initiative.TeamWithInitiative != this)
        {
            print("You Do Not Have Initiative");
            return;
        }
        if (!unitsOnTeam.Contains(enemySquad))
        {
            enemySquad.Movement.CentralizePosition();
            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].Combat.Attack(enemySquad);
            }
        }
    }
}
