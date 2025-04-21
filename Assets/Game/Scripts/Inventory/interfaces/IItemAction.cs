
using UnityEngine;

public abstract class IItemAction : ScriptableObject
{
    public abstract void Execute(RaycastHit hit);
}