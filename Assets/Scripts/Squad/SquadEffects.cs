using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadEffects
{
    public bool IsPinned { get; private set; } = false;
    public bool IsSuppressed { get; private set; } = false;

    Squad squad;
    List<Agent> Agents => squad.Agents;

    public SquadEffects(Squad squad)
    {
        this.squad = squad;
    }

    public void Pin()
    {
        if (!IsPinned && !IsSuppressed)
        {
            IsPinned = true;
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Effects.ShowPinMarker();
            }
        }
    }

    public void Suppress()
    {
        if (!IsSuppressed)
        {
            // Unpin to Prevent Double Markers
            if (IsPinned)
            {
                UnPin();
            }
            IsSuppressed = true;
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Effects.ShowSuppressionMarker();
            }
        }
    }

    private void UnPin()
    {
        IsPinned = false;
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Effects.HidePinMarker();
        }
    }

    private void UnSuppress()
    {
        IsSuppressed = false;
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Effects.HideSuppressionMarker();
        }
    }
}