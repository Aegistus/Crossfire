using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCombat
{
    public bool Attacking => Agents.Find(agent => agent.StateMachine.CurrentState.GetType() == typeof(Shooting)) != null;

    LineOfSight los;
    Squad squad;
    List<Agent> Agents => squad.Agents;

    Squad movingEnemySquad;

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
                if (Agents[i].Weapon != null)
                {
                    diceNum += Agents[i].Weapon.Stats.hitDice;
                }
            }
            int[] rolls = Dice.RollD6Multiple(diceNum);
            for (int i = 0; i < Agents.Count; i++)
            {
                Agents[i].Shoot(target.Movement.Position);
            }
            bool attackSuccessful = target.Combat.ReceiveDamage(rolls);
            if (!attackSuccessful)
            {
                squad.InitiativeFailure();
            }
        }
        else
        {
            Debug.Log("No LOS");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rolls"></param>
    /// <returns> Returns false if no rolls are hits, returns true if at least one roll is a hit. </returns>
    public bool ReceiveDamage(int[] rolls)
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
            else if (rolls[i] > 5)
            {
                hits++;
            }
        }
        Debug.Log("Hits: " + hits);
        if (hits > 1)
        {
            squad.Effects.Pin();
        }
        if (hits > 2)
        {
            squad.Effects.Suppress();
        }
        for (int i = 0; i < hits && i < Agents.Count; i++)
        {
            Agents[i].Kill();
        }
        Agents.RemoveAll(agent => agent.Health.IsAlive == false);
        if (Agents.Count == 0)
        {
            squad.Kill();
        }
        if (hits == 0)
        {
            return false;
        }
        return true;
    }

    public void UpdateMovingSquads(Squad movingEnemySquad)
    {
        this.movingEnemySquad = movingEnemySquad;
    }

    public void CheckForReactiveFire()
    {
        if (movingEnemySquad != null)
        {
            if (Vector3.Distance(movingEnemySquad.Movement.Position, squad.Movement.Position) < 20f
                && los.HasLineOfSight(squad.Movement.Position, movingEnemySquad.Movement.Position, 20f))
            {
                Attack(movingEnemySquad);
                movingEnemySquad = null;
            }
        }
    }

}
