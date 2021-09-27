using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientWeapon : MonoBehaviour
{
    public Transform weaponAnchor;

    private void Update()
    {
        transform.GetChild(0)?.LookAt(weaponAnchor);
    }
}
