using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldLandItemDatabase : MonoBehaviour
{
    public static WorldLandItemDatabase Instance;

    public List<LandItem> landitems = new List<LandItem>();
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

    public LandItem GetLandItem(int queryItemID)
    {
        return landitems.FirstOrDefault(item => item.itemID == queryItemID);
    }

}
