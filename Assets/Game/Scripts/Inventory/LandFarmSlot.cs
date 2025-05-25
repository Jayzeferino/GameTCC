using System.Collections.Generic;
using UnityEngine;

public class LandFarmSlot : MonoBehaviour
{
    public Transform parentOverride;
    public LandItem currentLandPlant;
    public GameObject currentHavestPlantModel;

    private int grow = 0;

    public void UnloadLand()
    {
        if (currentLandPlant != null)
        {
            currentHavestPlantModel.SetActive(false);
        }
    }

    public void UnloadLandAndDestroy()
    {
        if (currentHavestPlantModel != null)
        {
            Destroy(currentHavestPlantModel);
        }
    }

    public void LoadPlantlModel(LandItem plant)
    {

        UnloadLandAndDestroy();

        if (plant == null)
        {
            UnloadLand();
            return;
        }
        GameObject seed = Instantiate(plant.ModelPlantPhases[grow]) as GameObject;
        if (seed != null)
        {
            if (parentOverride != null)
            {
                seed.transform.parent = parentOverride;
            }
            else
            {
                seed.transform.parent = transform;
            }

            seed.transform.localPosition = Vector3.zero;
            seed.transform.localRotation = Quaternion.identity;
            seed.transform.localScale = Vector3.one;
            currentLandPlant = plant;
        }

        currentHavestPlantModel = seed;
    }


    public void SetGrow(int growTime)
    {
        grow = growTime;
    }
    public int GetGrow()
    {
        return grow;
    }
    public void GrowPlant()
    {
        if (grow < currentLandPlant.ModelPlantPhases.Count - 1)
        {
            grow++;
            LoadPlantlModel(currentLandPlant);
        }
    }
}
