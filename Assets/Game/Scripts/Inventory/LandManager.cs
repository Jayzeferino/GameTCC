using UnityEngine;

public class LandManager : MonoBehaviour, ITimeTracker
{
    [SerializeField] LandFarmSlot landSlot;
    Land land;
    public GameTimestamp timeToGrow;
    public bool hasPlant = false;

    private void Update()
    {

    }
    private void Awake()
    {
        landSlot = GetComponentInChildren<LandFarmSlot>();
        land = GetComponent<Land>();
    }

    private void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }
    public void LoadPlantOnSlot(LandItem landItem)
    {
        if (hasPlant == false)
        {
            landSlot.LoadPlantlModel(landItem);
            hasPlant = true;

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
            int growTime = GameTimestamp.CompareTimestampInMinutes(timeToGrow, timestamp);
            if (growTime == landSlot.currentLandPlant.minutesToGrow)
            {
                landSlot.GrowPlant();
                timeToGrow.realElapsedTime = timestamp.realElapsedTime;
            }

        }
    }
}
