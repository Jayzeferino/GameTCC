using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    LandFarmSlot landSlot;
    public bool hasPlant = false;
    private void Awake()
    {
        landSlot = GetComponentInChildren<LandFarmSlot>();
    }
    public void LoadPlantOnSlot(LandItem landItem)
    {
        landSlot.LoadPlantlModel(landItem);
        hasPlant = true;
    }
    public void UnLoadPlantOnSlot()
    {
        landSlot.UnloadLand();
        hasPlant = false;
    }


}
