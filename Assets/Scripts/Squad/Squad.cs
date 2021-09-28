using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Squad : MonoBehaviour
{
    [SerializeField]
    private Vector2[] agentPositions;

    public event Action OnSquadInitiativeFailure;

    public SquadSelection Selection { get; private set; }
    public SquadMovement Movement { get; private set; }
    public SquadCombat Combat { get; private set; }
    public SquadEffects Effects { get; private set; }

    public bool Ready => !Movement.IsMoving && !Combat.Attacking;
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
        StartCoroutine(ReactiveFireCheck());
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

    private void Update()
    {
        if (Movement.IsMoving)
        {
            Movement.CentralizePosition();
        }
    }

    private IEnumerator ReactiveFireCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Combat.CheckForReactiveFire();
        }
    }

    public void InitiativeFailure()
    {
        OnSquadInitiativeFailure?.Invoke();
    }

}
