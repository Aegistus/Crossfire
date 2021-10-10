using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMarker : MonoBehaviour
{
    public float cullDistance = 5f;

    private Transform camTransform;
    private Squad squad;
    private GameObject childIcon;

    private void Start()
    {
        camTransform = Camera.main.transform;
        squad = GetComponentInParent<Squad>();
        childIcon = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (Mathf.Abs(camTransform.position.y - transform.position.y) < cullDistance)
        {
            childIcon.SetActive(false);
        }
        else if (!childIcon.activeInHierarchy)
        {
            childIcon.SetActive(true);
        }
        transform.position = new Vector3(squad.Movement.Position.x, squad.Movement.Position.y + 3, squad.Movement.Position.z);
        transform.LookAt(camTransform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
