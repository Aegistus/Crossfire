using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCombat
{
    LineOfSight los;

    Squad squad;
    List<Agent> Agents => squad.Agents;

    public SquadCombat(Squad squad, LineOfSight los)
    {
        this.squad = squad;
        this.los = los;
    }

    public void Attack(Squad target)
    {
        if (squad.Effects.IsSuppressed)
        {
            return;
        }
        squad.Movement.CentralizePosition();
        if (los.HasLineOfSight(squad.Movement.Position, target.Movement.Position, 100f))
        {
            int diceNum = 0;
            for (int i = 0; i < Agents.Count; i++)
            {
                diceNum += Agents[i].Weapon.Stats.hitDice;
            }
            int[] rolls = Dice.RollD6Multiple(diceNum);
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Shoot();
            }
            target.Combat.ReceiveDamage(rolls);
        }
        else
        {
            Debug.Log("No LOS");
        }
    }

    public void ReceiveDamage(int[] rolls)
    {
        int hits = 0;
        for (int i = 0; i < rolls.Length; i++)
        {
            if (squad.Cover == CoverType.FullCover && rolls[i] > 5)
            {
                hits++;
            }
            else if (squad.Cover == CoverType.HalfCover && rolls[i] > 4)
            {
                hits++;
            }
            else if (rolls[i] > 3)
            {
                hits++;
            }
        }
        Debug.Log("Hits: " + hits);
        for (int i = 0; i < hits && i < Agents.Count; i++)
        {
            Agents[i].Kill();
        }
        Agents.RemoveAll(agent => agent.Health.IsAlive == false);
        if (hits > 1)
        {
            squad.Effects.Pin();
        }
        if (hits > 3)
        {
            squad.Effects.Suppress();
        }
    }
}
