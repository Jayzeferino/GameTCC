using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    LandFarmSlot landSlot;

    Land land;
    public bool hasPlant = false;
    private void Awake()
    {
        landSlot = GetComponentInChildren<LandFarmSlot>();
        land = GetComponent<Land>();
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
}
