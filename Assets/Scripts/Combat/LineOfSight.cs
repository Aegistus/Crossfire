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

    RaycastHit[] hits = new RaycastHit[20];
    float distance;
    public bool HasLineOfSight(Vector3 origin, Vector3 target, float range)
    {
        distance = Vector3.Distance(origin, target);
        if (distance <= range)
        {
            Vector3 heading = (target - origin).normalized;
            int hitNumber = Physics.RaycastNonAlloc(origin, heading, hits, lineOfSightLayers);
            print("Hit Count: " + hits.Length);
            for (int i = 0; i < hitNumber; i++)
            {
                if (hits[i].distance < distance)
                {
                    print("Hit: " + hits[i].collider.gameObject.name);
                    return false;
                }
            }
            return true;
        }
        else
        {
            print("Out of range");
            return false;
        }
    }

}
