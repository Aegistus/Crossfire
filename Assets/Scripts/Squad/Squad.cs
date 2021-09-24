using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Squad : MonoBehaviour, ICommandable
{
    public Vector2[] agentPositions;

    public bool IsPinned { get; private set; } = false;
    public bool IsSuppressed { get; private set; } = false;
    public CoverType Cover => GetCurrentCover();
    public Vector3 Position { get; private set; }

    private List<Agent> agents;
    private LineOfSight los;

    private void Start()
    {
        agents = GetComponentsInChildren<Agent>().ToList();
        CentralizePosition();
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Selection.Deselect();
        }
        los = LineOfSight.Instance;
    }

    public void CentralizePosition()
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            center += agents[i].transform.position;
        }
        center /= agents.Count;
        center.y += 2;
        Position = center;
    }

    private CoverType GetCurrentCover()
    {
        if (agents.Find(agent => agent.Cover.CoverType == CoverType.FullCover) != null)
        {
            return CoverType.FullCover;
        }
        else if (agents.Find(agent => agent.Cover.CoverType == CoverType.HalfCover) != null)
        {
            return CoverType.HalfCover;
        }
        else
        {
            return CoverType.NoCover;
        }
    }

    public void MoveAgentsIntoFormation()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if (i < agentPositions.Length)
            {
                agents[i].Movement.SetDestination(new Vector3(Position.x + agentPositions[i].x, Position.y, Position.z + agentPositions[i].y));
            }
        }
    }

    public void Select()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Selection.Select();
        }
    }

    public void Deselect()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Selection.Deselect();
        }
    }

    public void Move(Vector3 destination)
    {
        if (IsPinned || IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Movement.SetDestination(new Vector3(destination.x + agentPositions[i].x, destination.y, destination.z + agentPositions[i].y));
        }
    }

    public void MoveToCover(Cover cover)
    {
        if (IsPinned || IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Cover.EnterCover(cover);
            agents[i].Movement.SetDestination(agents[i].Cover.Position);
        }
    }

    public void MoveOutOfCover()
    {
        if (IsPinned || IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Cover.ExitCover();
        }
    }

    public void Attack(Squad target)
    {
        if (IsSuppressed)
        {
            return;
        }
        CentralizePosition();
        if (los.HasLineOfSight(Position, target.Position, 100f))
        {
            print("Has LOS");
            int diceNum = 0;
            for (int i = 0; i < agents.Count; i++)
            {
                diceNum += agents[i].Weapon.Stats.hitDice;
            }
            int[] rolls = Dice.RollD6Multiple(diceNum);
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Shoot();
            }
            target.ReceiveDamage(rolls);
        }
        else
        {
            print("No LOS");
        }
    }

    public void ReceiveDamage(int[] rolls)
    {
        int hits = 0;
        for (int i = 0; i < rolls.Length; i++)
        {
            if (Cover == CoverType.FullCover && rolls[i] > 5)
            {
                hits++;
            }
            else if (Cover == CoverType.HalfCover && rolls[i] > 4)
            {
                hits++;
            }
            else if (rolls[i] > 3)
            {
                hits++;
            }
        }
        print("Hits: " + hits);
        for (int i = 0; i < hits && i < agents.Count; i++)
        {
            agents[i].Kill();
        }
        agents.RemoveAll(agent => agent.Health.IsAlive == false);
        if (hits > 1)
        {
            PinSquad();
        }
        if (hits > 3)
        {
            SuppressSquad();
        }
    }

    private void PinSquad()
    {
        if (!IsPinned && !IsSuppressed)
        {
            IsPinned = true;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Effects.ShowPinMarker();
            }
        }
    }

    private void SuppressSquad()
    {
        if (!IsSuppressed)
        {
            // Unpin to Prevent Double Markers
            if (IsPinned)
            {
                IsPinned = false;
                for (int i = 0; i < agents.Count; i++)
                {
                    agents[i].Effects.HidePinMarker();
                }
            }
            IsSuppressed = true;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Effects.ShowSuppressionMarker();
            }
        }
    }
}
