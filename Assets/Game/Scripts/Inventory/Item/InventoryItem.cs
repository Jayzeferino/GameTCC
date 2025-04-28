using UnityEngine;

public abstract class InvetoryItem : Item
{
    public GameObject modelPrefab;
    [Header("Tool Interaction Animation")]
    public string ACTION_TOOL;

    public abstract void UseItem(RaycastHit hit);
}