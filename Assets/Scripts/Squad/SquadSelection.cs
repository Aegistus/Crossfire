using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelection
{
    Squad squad;
    List<Agent> Agents => squad.Agents;

    public SquadSelection(Squad squad)
    {
        this.squad = squad;
    }

    public void Select()
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Selection.Select();
        }
    }

    public void Deselect()
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Selection.Deselect();
        }
    }
}
