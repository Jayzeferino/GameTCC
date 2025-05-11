using System;
using UnityEngine;

public class LandManager : MonoBehaviour, ITimeTracker
{
    LandFarmSlot landSlot;
    Land land;
    public GameTimestamp growingTime;
    public bool hasPlant = false;
    private void Awake()
    {
        landSlot = GetComponentInChildren<LandFarmSlot>();
        land = GetComponent<Land>();
    }

    private void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
        if (this.isActiveAndEnabled)
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

    public LandManagerSaveData GetLandManagerSaveData()
    {

        LandManagerSaveData landSaveData = new();
        landSaveData.hasPlant = hasPlant;
        landSaveData.currentGrowingTimestamp = growingTime.realElapsedTime.ToString();
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
        }
        landSaveData.landSaveData = land.GetLandSaveData();

        return landSaveData;

    }

    public void SetLandManagerSaveData(LandManagerSaveData landSaveData)
    {
        hasPlant = landSaveData.hasPlant;
        LoadPlantOnSlot(WorldLandItemDatabase.Instance.GetLandItem(landSaveData.landItemId));
        // transform.position = new Vector3(landSaveData.xPosition, landSaveData.yPosition, landSaveData.zPosition);
        growingTime.gameStartTime = DateTime.Parse(landSaveData.startGrowTime);
        growingTime.realElapsedTime = TimeSpan.Parse(landSaveData.currentGrowingTimestamp);
        land.dryTime.gameStartTime = DateTime.Parse(landSaveData.landSaveData.startWateredTime);
        land.dryTime.realElapsedTime = TimeSpan.Parse(landSaveData.landSaveData.currentDryTimestamp);
        land.landStatus = landSaveData.landSaveData.landStatus;
    }

}
