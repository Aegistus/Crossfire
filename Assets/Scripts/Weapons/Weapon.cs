using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats stats;

    public WeaponStats Stats { get => stats; }
}
