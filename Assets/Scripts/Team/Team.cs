using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    public List<Squad> unitsOnTeam = new List<Squad>();

    public bool HasUnitsSelected => selectedUnits.Count > 0;

    private List<Squad> selectedUnits = new List<Squad>();

    private void Awake()
    {
        Initiative.AddTeam(this);
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
