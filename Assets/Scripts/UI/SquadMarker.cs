using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMarker : MonoBehaviour
{
    private Transform camTransform;
    private Squad squad;

    private void Start()
    {
        camTransform = Camera.main.transform;
        squad = GetComponentInParent<Squad>();
    }

    private void Update()
    {
        transform.position = squad.Movement.Position;
        transform.LookAt(camTransform);
    }
}
