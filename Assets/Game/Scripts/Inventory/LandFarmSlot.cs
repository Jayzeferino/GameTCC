using System.Collections.Generic;
using UnityEngine;

public class LandFarmSlot : MonoBehaviour
{
    public Transform parentOverride;
    public LandItem currentLandPlant;
    public GameObject currentHavestPlantModel;

    private int evolve = 0;
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
        GameObject seed = Instantiate(plant.ModelPlantPhases[evolve]) as GameObject;
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

    public void EvolvePlant()
    {
        evolve++;
        LoadPlantlModel(currentLandPlant);

    }
}