using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponStats stats;
    [SerializeField]
    private ParticleSystem[] gunSmokeParticles;
    [SerializeField]
    private ParticleSystem[] shotParticles;

    public WeaponStats Stats { get => stats; }

    public void Shoot()
    {
        if (shotParticles != null)
        {
            StartCoroutine(RepeatedShots());
        }
        if (gunSmokeParticles != null)
        {
            for (int i = 0; i < gunSmokeParticles.Length; i++)
            {
                gunSmokeParticles[i].Play(true);
            }
        }
    }

    public IEnumerator RepeatedShots()
    {
        int shotsFired = 0;
        while (shotsFired < stats.shotBursts)
        {
            for (int i = 0; i < shotParticles.Length; i++)
            {
                shotParticles[i].Play(true);
            }
            yield return new WaitForSeconds(stats.shotInterval);
            shotsFired++;
        }
    }
}
