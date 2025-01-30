using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{


    private Renderer render;
    public float valorMolhado = 1.09f;
    public float valorSoloSaudavel = 0.70f;
    public float valorSoloSeco = 0.4f;

    public int TimeWaterToExpireInHours = 24;

    public GameTimestamp timeWatered;

    public enum LandStatus
    {
        Dead, Farmland, Watered
    }

    public LandStatus landStatus;

    private void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Farmland);
        TimeManager.Instance.RegisterTracker(this);
    }

    public void SwitchLandStatus(LandStatus statusSwitch)
    {
        landStatus = statusSwitch;
        switch (statusSwitch)
        {
            case LandStatus.Dead:
                ChangeMaterialColor(valorSoloSeco);
                break;
            case LandStatus.Farmland:
                ChangeMaterialColor(valorSoloSaudavel);
                break;
            case LandStatus.Watered:
                ChangeMaterialColor(valorMolhado);
                timeWatered = TimeManager.Instance.GetGameTimestamp();
                break;
        }
    }

    private void ChangeMaterialColor(float tilingX)
    {
        // If the prefab has a renderer and material
        if (render != null && render.material != null && render.material.HasProperty("_MainTex") == true)
        {
            // Change the tiling of the material
            render.material.mainTextureScale = new Vector2(tilingX, 1);
        }
        else
        {
            Debug.LogWarning("Prefab does not have a Renderer or Material!");
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        if (landStatus == LandStatus.Watered)
        {

            int timeElapsed = GameTimestamp.CompareTimestampInHours(timeWatered, timestamp);

            if (timeElapsed > TimeWaterToExpireInHours)
            {
                SwitchLandStatus(Land.LandStatus.Farmland);
                timeWatered.StartClock();
            }

        }
    }
}
