using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats stats;
    [SerializeField]
    private ParticleSystem[] shotParticles;

    public WeaponStats Stats { get => stats; }

    public void Fire()
    {
        if (shotParticles != null)
        {
            for (int i = 0; i < shotParticles.Length; i++)
            {
                shotParticles[i].Play();
            }
        }
    }
}
