using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLandSaveManager : MonoBehaviour
{
    public static WorldLandSaveManager Instance;
    public int lastLandId = 0;
    public GameObject landToInstantiate;
    public List<LandManager> lands;
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

    public void SaveLandData(List<LandManagerSaveData> landDataList)
    {
        LandManagerSaveDataListWrapper wrapper = new()
        {
            list = landDataList
        };

        string json = JsonUtility.ToJson(wrapper);

        Debug.Log("Saving Land Data: " + json.ToString());
        PlayerPrefs.SetString("LandData", json);
        PlayerPrefs.Save();
    }

    public void LandManagerSaveDataToJson()
    {
        SaveLandData(GetLandManagerSaveDataList());

    }

    public List<LandManagerSaveData> LoadLandData()
    {
        string json = PlayerPrefs.GetString("LandData", "");
        if (string.IsNullOrEmpty(json)) return new List<LandManagerSaveData>();

        LandManagerSaveDataListWrapper wrapper = JsonUtility.FromJson<LandManagerSaveDataListWrapper>(json);
        return wrapper.list;
    }

    public void InstanciateAndLoadLandManagerSaveDataList(List<LandManagerSaveData> landSaveData)
    {
        lands = new();
        foreach (LandManagerSaveData landSaveManagerData in landSaveData)
        {
            GameObject crop = Instantiate(landToInstantiate, new Vector3(landSaveManagerData.xPosition, landSaveManagerData.yPosition, landSaveManagerData.zPosition), Quaternion.identity);
            LandManager landManager = crop.GetComponent<LandManager>();
            landManager.previewLand = false;
            if (landSaveManagerData.hasPlant == true)
            {
                landManager.SetLandManagerSaveData(landSaveManagerData);
            }
        }
    }

    public void DeleteLandFromManager(int landID)
    {
        LandManager landManager = lands.Find(l => l.landId == landID);
        if (landManager != null)
        {
            lands.Remove(landManager);
        }

        UpdateIDAfterDelete(landID);
    }

    public void UpdateIDAfterDelete(int landID)
    {
        if (landID >= lastLandId)
        {
            lastLandId = landID + 1;
        }
    }

}
