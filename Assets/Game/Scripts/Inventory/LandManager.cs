using System;
using UnityEngine;

public class LandManager : MonoBehaviour, ITimeTracker
{
    LandFarmSlot landSlot;
    Land land;
    public GameTimestamp growingTime;
    public bool hasPlant = false;
    public bool previewLand = true;

    private void Awake()
    {
        landSlot = GetComponentInChildren<LandFarmSlot>();
        land = GetComponent<Land>();
    }

    private void Start()
    {
        TimeManager.Instance.RegisterTracker(this);

        if (previewLand == false)
        {
            WorldLandSaveManager.Instance.RegisterLandManager(this);
        }
    }

    public void LoadPlantOnSlot(LandItem landItem)
    {
        if (hasPlant == false)
        {
            landSlot.LoadPlantlModel(landItem);
            hasPlant = true;
            growingTime.StartClock();
        }
    }
    public void UnLoadPlantOnSlot()
    {
        landSlot.UnloadLand();
        hasPlant = false;
    }
    public void WaterFarmLand()
    {
        if (hasPlant)
        {
            land.SwitchLandStatus(Land.LandStatus.Watered);
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if (land.landStatus == Land.LandStatus.Watered && hasPlant && landSlot != null)
        {
            int growTime = GameTimestamp.CompareTimestampInMinutes(growingTime, timestamp);

            if (growTime == landSlot.currentLandPlant.minutesToGrow)
            {
                landSlot.GrowPlant();
                growingTime.realElapsedTime = timestamp.realElapsedTime;
            }

        }
    }

    public void UpdateGrowState()
    {
        GameTimestamp gameTimestamp = TimeManager.Instance.GetGameTimestamp();

        int growTime = GameTimestamp.CompareTimestampInMinutes(growingTime, gameTimestamp);
        int growState = growTime / landSlot.currentLandPlant.minutesToGrow;

        Debug.Log("elapsed time: " + growTime);
        Debug.Log("plants grows stages: " + growTime / landSlot.currentLandPlant.minutesToGrow);

        for (int i = 0; i < growState; i++)
        {
            if (i < landSlot.currentLandPlant.ModelPlantPhases.Count)
            {
                Debug.Log("GROWINNG ...");
                landSlot.GrowPlant();
            }
        }

    }

    public LandManagerSaveData GetLandManagerSaveData()
    {

        LandManagerSaveData landSaveData = new();
        landSaveData.hasPlant = hasPlant;
        landSaveData.startGrowTime = growingTime.gameStartTime.ToString();
        if (landSlot.currentLandPlant != null)
        {
            landSaveData.landItemId = landSlot.currentLandPlant.itemID;
        }
        landSaveData.xPosition = transform.position.x;
        landSaveData.yPosition = transform.position.y;
        landSaveData.zPosition = transform.position.z;
        if (landSlot.currentLandPlant != null)
        {
            landSaveData.landItemId = landSlot.currentLandPlant.itemID;
            landSaveData.grow = landSlot.GetGrow();
        }
        landSaveData.landSaveData = land.GetLandSaveData();

        return landSaveData;

    }

    public void SetLandManagerSaveData(LandManagerSaveData landSaveData)
    {
        hasPlant = landSaveData.hasPlant;
        transform.position = new Vector3(landSaveData.xPosition, landSaveData.yPosition, landSaveData.zPosition);
        growingTime.gameStartTime = DateTime.Parse(landSaveData.startGrowTime);
        landSlot.SetGrow(landSaveData.grow);
        land.SetLandFromSaveData(landSaveData.landSaveData);
        landSlot.LoadPlantlModel(WorldLandItemDatabase.Instance.GetLandItem(landSaveData.landItemId));
        growingTime.RestoreGameTime(growingTime.gameStartTime);
        UpdateGrowState();
    }

}
