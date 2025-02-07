using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        Tool, Seed, fruit
    }



    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;

    public ItemType type;
}
