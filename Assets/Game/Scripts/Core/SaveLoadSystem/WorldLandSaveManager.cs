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
        }
        else
        {
            Destroy(gameObject);
        }
        lands = new();
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

    // public void InstanciateAndLoadLandManagerSaveDataList(List<LandManagerSaveData> landSaveData)
    // {

    //     Debug.Log("LANDSAVEDATA " + landSaveData.Count);
    //     // foreach (LandManagerSaveData landManager in landSaveData)
    //     // {

    //     //     // Instantiate(landToInstantiate, new Vector3(landManager.xPosition, landManager.yPosition, landManager.zPosition), Quaternion.identity);
    //     // }
    // }

    // public void LoadLandSaveDataList(List<LandSaveData> landSaveData)
    // {

    //     for (int i = 0; i < lands.Count; i++)
    //     {
    //         lands[i].SetLandSaveData(landSaveData[i]);
    //     }
    // }
}
