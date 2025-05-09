[System.Serializable]
public class InventoryItemDict
{
    public int slot;
    public int itemID;

    public InventoryItemDict(int k, int v)
    {
        slot = k;
        itemID = v;
    }
}