using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Squad : MonoBehaviour
{
    [SerializeField]
    private Vector2[] agentPositions;

    public SquadSelection Selection { get; private set; }
    public SquadMovement Movement { get; private set; }
    public SquadCombat Combat { get; private set; }
    public SquadEffects Effects { get; private set; }

    public CoverType Cover => GetCurrentCover();
    public List<Agent> Agents { get; private set; }

    private LineOfSight los;

    private void Start()
    {
        Agents = GetComponentsInChildren<Agent>().ToList();
        los = LineOfSight.Instance;

        Selection = new SquadSelection(this);
        Movement = new SquadMovement(this, agentPositions);
        Combat = new SquadCombat(this, los);
        Effects = new SquadEffects(this);

        Movement.CentralizePosition();
        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].Selection.Deselect();
        }
    }

    private CoverType GetCurrentCover()
    {
        if (Agents.Find(agent => agent.Cover.CoverType == CoverType.FullCover) != null)
        {
            return CoverType.FullCover;
        }
        else if (Agents.Find(agent => agent.Cover.CoverType == CoverType.HalfCover) != null)
        {
            return CoverType.HalfCover;
        }
        else
        {
            return CoverType.NoCover;
        }
    }

}
