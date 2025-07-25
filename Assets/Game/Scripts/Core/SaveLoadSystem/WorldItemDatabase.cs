using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDatabase : MonoBehaviour
{
    public static WorldItemDatabase Instance;

    public List<InvetoryItem> invetoryitems = new List<InvetoryItem>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public InvetoryItem GetInvetoryItem(int queryItemID)
    {
        return invetoryitems.FirstOrDefault(item => item.itemID == queryItemID);
    }


}
