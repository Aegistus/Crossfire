using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandable
{
    bool InCover { get; }
    void Move(Vector3 position);
    void Select();
    void Deselect();
    void MoveToCover(Cover cover);
    void MoveOutOfCover();
}
