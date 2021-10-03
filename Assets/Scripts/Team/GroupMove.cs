using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMove
{
    public bool Active { get; private set; } = false;
    public Squad InitiatingSquad { get; private set; }

    private List<Squad> participatingSquads = new List<Squad>();
    private Dictionary<Squad, Vector3> movements = new Dictionary<Squad, Vector3>();
    private Dictionary<Squad, Cover> coverMovements = new Dictionary<Squad, Cover>();

    public void StartNewGroupMove(Squad initiatingSquad)
    {
        InitiatingSquad = initiatingSquad;
        Active = true;
        participatingSquads.Clear();
        movements.Clear();
        coverMovements.Clear();
        Debug.Log("Starting Group Move");
    }

    public void AddMovement(Squad squad, Vector3 position)
    {
        if (!movements.ContainsKey(squad) && !coverMovements.ContainsKey(squad))
        {
            movements.Add(squad, position);
        }
    }

    public void AddCoverMovement(Squad squad, Cover cover)
    {
        if (!movements.ContainsKey(squad) && !coverMovements.ContainsKey(squad))
        {
            coverMovements.Add(squad, cover);
        }
    }

    public void ExecuteMove()
    {
        foreach (var squad in movements.Keys)
        {
            squad.Movement.Move(movements[squad]);
        }
        foreach (var squad in coverMovements.Keys)
        {
            squad.Movement.MoveToCover(coverMovements[squad]);
        }
    }

}
