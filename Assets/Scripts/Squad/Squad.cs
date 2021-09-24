using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Squad : MonoBehaviour, ICommandable
{
    public Vector2[] agentPositions;

    public CoverType Cover => GetCurrentCover();
    public Vector3 Position { get; private set; }

    private List<Agent> agents;

    private void Start()
    {
        agents = GetComponentsInChildren<Agent>().ToList();
        CentralizePosition();
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Selection.Deselect();
        }
    }

    private void CentralizePosition()
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < agents.Count; i++)
        {
            center += agents[i].transform.position;
        }
        center /= agents.Count;
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
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Movement.SetDestination(new Vector3(destination.x + agentPositions[i].x, destination.y, destination.z + agentPositions[i].y));
        }
    }

    public void MoveToCover(Cover cover)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Cover.EnterCover(cover);
            agents[i].Movement.SetDestination(agents[i].Cover.Position);
        }
    }

    public void MoveOutOfCover()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].Cover.ExitCover();
        }
    }

    public void Attack(Squad target)
    {
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
        for (int i = 0; i < hits && i < agents.Count; i++)
        {
            agents[i].Kill();
        }
        agents.RemoveAll(agent => agent.Health.IsAlive == false);
    }
}
