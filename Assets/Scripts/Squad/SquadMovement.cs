using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement
{
    public bool IsMoving => Agents.Find(agent => agent.StateMachine.CurrentState.GetType() == typeof(Walking)) != null;
    public Vector3 Position { get; private set; }

    Squad squad;
    List<Agent> Agents => squad.Agents;
    Vector2[] agentPositions;

    public SquadMovement(Squad squad, Vector2[] agentPositions)
    {
        this.squad = squad;
        this.agentPositions = agentPositions;
    }

    public void CentralizePosition()
    {
        Vector3 center = Vector3.zero;
        for (int i = 0; i < Agents.Count; i++)
        {
            center += Agents[i].transform.position;
        }
        center /= Agents.Count;
        center.y += 2;
        Position = center;
    }

    public void MoveAgentsIntoFormation()
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            if (i < agentPositions.Length)
            {
                Agents[i].Movement.SetDestination(new Vector3(Position.x + agentPositions[i].x, Position.y, Position.z + agentPositions[i].y));
            }
        }
    }

    public void Move(Vector3 destination)
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Movement.SetDestination(new Vector3(destination.x + agentPositions[i].x, destination.y, destination.z + agentPositions[i].y));
        }
    }

    public void MoveToCover(Cover cover)
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Cover.EnterCover(cover);
            Agents[i].Movement.SetDestination(Agents[i].Cover.Position);
        }
    }

    public void MoveOutOfCover()
    {
        if (squad.Effects.IsPinned || squad.Effects.IsSuppressed)
        {
            return;
        }
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Cover.ExitCover();
        }
    }

    public void Stop()
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Movement.Stop();
        }
    }

    public void CalculatePath(Vector3 destination)
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Movement.CalculatePath(new Vector3(destination.x + agentPositions[i].x, destination.y, destination.z + agentPositions[i].y));
        }
    }
}
