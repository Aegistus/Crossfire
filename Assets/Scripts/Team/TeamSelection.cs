using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamSelection
{
    public event Action<Squad> OnSelection;
    public event Action<Squad> OnDeselection;

    public bool HasUnitsSelected => SelectedUnits.Count > 0;

    public List<Squad> SelectedUnits { get; private set; } = new List<Squad>();

    private Team team;

    public TeamSelection(Team team)
    {
        this.team = team;
    }

    public void SelectSquad(Squad toSelect)
    {
        if (!team.AllSquadsReady)
        {
            return;
        }
        if (Initiative.TeamWithInitiative != team)
        {
            Debug.Log("You Do Not Have Initiative");
            return;
        }
        if (team.SquadIsOnTeam(toSelect))
        {
            toSelect.Selection.Select();
            SelectedUnits.Add(toSelect);
            OnSelection?.Invoke(toSelect);
        }
        else
        {
            Debug.Log("Unit Not on Team");
        }
    }

    public void DeselectSquad(Squad toDeselect)
    {
        if (!team.AllSquadsReady)
        {
            return;
        }
        toDeselect.Selection.Deselect();
        SelectedUnits.Remove(toDeselect);
        OnDeselection?.Invoke(toDeselect);
    }

    public void DeselectAll()
    {
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            SelectedUnits[i].Selection.Deselect();
            OnDeselection?.Invoke(SelectedUnits[i]);
        }
        SelectedUnits.Clear();
    }
}
