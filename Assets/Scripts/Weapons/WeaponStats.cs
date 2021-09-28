using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon Stat")]
public class WeaponStats : ScriptableObject
{
    public int hitDice = 1;

    [Header("Particle Effects")]
    public int shotBursts = 1;
    public float shotInterval = .1f;
}
