using UnityEngine;

public class Item : ScriptableObject
{
    public int itemID;

    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;

    public int mathLvRequired;
    public int portLvRequired;
    public IItemAction itemAction;

}
