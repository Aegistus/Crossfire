using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandable
{
    void Move(Vector3 position);
    void Select();
    void Deselect();
}
