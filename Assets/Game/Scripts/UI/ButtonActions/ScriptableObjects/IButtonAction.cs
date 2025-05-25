
using UnityEngine;

public abstract class IButtonAction : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public abstract void Execute(RaycastHit hit);
}