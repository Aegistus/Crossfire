using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTeam : Team
{
    public LayerMask selectablesLayer;
    public LayerMask groundLayer;
    public LayerMask coverLayer;

    Camera mainCam;

    private void Start()
    {
        Initiative.StartGame();
        mainCam = Camera.main;
    }

    Cover currentCoverUnderCursor;
    RaycastHit squadRayHit;
    RaycastHit coverRayHit;
    RaycastHit groundRayHit;
    private void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        // left clicks
        if (Input.GetMouseButtonDown(0))
        {
            print("LMB");
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out squadRayHit, 100f, selectablesLayer, QueryTriggerInteraction.Collide);
            HandleLeftClick(squadRayHit.collider?.GetComponentInParent<Squad>());
        }

        // right clicks
        if (Input.GetMouseButtonUp(1))
        {
            print("RMB");
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out squadRayHit, 100f, selectablesLayer);
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out coverRayHit, 100f, coverLayer);
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out groundRayHit, 100f, groundLayer);
            if (squadRayHit.collider != null)
            {
                HandleRightClick(squadRayHit.collider.GetComponentInParent<Squad>());
            }
            else if (coverRayHit.collider != null)
            {
                HandleRightClick(coverRayHit.collider.GetComponentInParent<Cover>());
            }
            else if (groundRayHit.collider != null)
            {
                HandleRightClick(groundRayHit.point);
            }
        }

        // show cover positions when units selected
        if (Selection.HasUnitsSelected)
        {
            Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out coverRayHit, 100f, coverLayer);
            Cover cover = null;
            if (coverRayHit.collider != null)
            {
                cover = coverRayHit.collider.GetComponentInParent<Cover>();
            }
            if (coverRayHit.collider == null || cover == null || currentCoverUnderCursor != cover)
            {
                currentCoverUnderCursor?.HideCoverIcons();
            }
            currentCoverUnderCursor = cover;
            currentCoverUnderCursor?.ShowCoverIcons();
        }
    }

    public void HandleLeftClick(Squad squad)
    {
        if (squad != null)
        {
            Selection.DeselectAll();
            Selection.SelectSquad(squad);
        }
        else
        {
            Selection.DeselectAll();
        }
    }

    public void HandleRightClick(Squad squad)
    {
        if (squad != null)
        {
            Orders.GiveAttackOrderOnTarget(squad);
        }
    }

    public void HandleRightClick(Cover cover)
    {
        if (!GroupMovement.Active)
        {
            Orders.GiveMoveToCoverOrder(cover);
        }
        else
        {
            GroupMovement.AddCoverMovement(Selection.SelectedUnits[0], cover);
        }
    }

    public void HandleRightClick(Vector3 position)
    {
        if (!GroupMovement.Active)
        {
            Orders.GiveMoveOrder(position);
        }
        else
        {
            GroupMovement.AddMovement(Selection.SelectedUnits[0], position);
        }
    }
}
