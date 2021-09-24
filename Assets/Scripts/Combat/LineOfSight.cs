using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public LayerMask lineOfSightLayers;

    public static LineOfSight Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    static RaycastHit[] hits;
    static float distance;
    public static bool HasLineOfSight(Transform origin, Transform target, float range)
    {
        distance = Vector3.Distance(origin.position, target.position);
        if (distance <= range)
        {
            Vector3 heading = (target.position - origin.position).normalized;
            Physics.RaycastNonAlloc(origin.position, heading, hits, Instance.lineOfSightLayers);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < distance)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

}
