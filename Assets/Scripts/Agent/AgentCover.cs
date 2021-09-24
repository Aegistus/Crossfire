using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCover
{
    public Vector3 Position { get => currentCoverTransform.position; }
    public CoverType CoverType => currentCover == null ? CoverType.NoCover : currentCover.Type;
    private Cover currentCover;
    private Transform currentCoverTransform;

    public void EnterCover(Cover cover)
    {
        currentCover = cover;
        currentCoverTransform = cover.GetCoverPosition();
    }

    public void ExitCover()
    {
        if (currentCover != null)
        {
            currentCover.ReturnCoverPosition(currentCoverTransform);
            currentCover = null;
        }
    }
}
