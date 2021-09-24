using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientWeapon : MonoBehaviour
{
    public Transform otherHand;

    private void Update()
    {
        transform.LookAt(otherHand);
    }
}
