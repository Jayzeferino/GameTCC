using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLandSaveManager : MonoBehaviour
{
    public static WorldLandSaveManager Instance;
    public GameObject landToInstantiate;
    public List<LandManager> lands;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            lands = new();

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegisterLandManager(LandManager landManager)
    {
        lands.Add(landManager);
    }

    public void UnregisterLandManager(LandManager landManager)
    {
        lands.Remove(landManager);
    }

    public List<LandManagerSaveData> GetLandManagerSaveDataList()
    {
        List<LandManagerSaveData> landSaveData = new();
        foreach (LandManager landManager in lands)
        {
            landSaveData.Add(landManager.GetLandManagerSaveData());
        }
        return landSaveData;
    }

    public void InstanciateAndLoadLandManagerSaveDataList(List<LandManagerSaveData> landSaveData)
    {
        lands = new();
        foreach (LandManagerSaveData landSaveManagerData in landSaveData)
        {
            GameObject crop = Instantiate(landToInstantiate, new Vector3(landSaveManagerData.xPosition, landSaveManagerData.yPosition, landSaveManagerData.zPosition), Quaternion.identity);
            LandManager landManager = crop.GetComponent<LandManager>();
            landManager.previewLand = false;
            // landManager.LoadPlantOnSlot(WorldLandItemDatabase.Instance.GetLandItem(landSaveManagerData.landItemId));
            landManager.SetLandManagerSaveData(landSaveManagerData);

        }
    }
}
