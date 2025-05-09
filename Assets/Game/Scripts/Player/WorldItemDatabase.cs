using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDatabase : MonoBehaviour
{
    public static WorldItemDatabase Instance;

    public List<InvetoryItem> invetoryitems = new List<InvetoryItem>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public InvetoryItem GetInvetoryItem(int queryItemID)
    {
        return invetoryitems.FirstOrDefault(item => item.itemID == queryItemID);
    }


}
