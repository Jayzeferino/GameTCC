using System;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    private Renderer render;
    public float valorMolhado = 1.09f;
    public float valorSoloSaudavel = 0.70f;
    public float valorSoloSeco = 0.4f;

    public int TimeWaterToExpireInHours = 24;

    public GameTimestamp dryTime;

    public enum LandStatus
    {
        Dead, Farmland, Watered
    }

    public LandStatus landStatus;

    private void Awake()
    {
        render = gameObject.GetComponent<Renderer>();
    }
    private void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

    private void Update()
    {
        UpdateLandStatus(landStatus);
    }
    public void UpdateLandStatus(LandStatus statusSwitch)
    {
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
                break;
        }
    }
    public void SwitchLandStatus(LandStatus statusSwitch)
    {
        landStatus = statusSwitch;
        switch (statusSwitch)
        {
            case LandStatus.Dead:

                break;
            case LandStatus.Farmland:
                break;
            case LandStatus.Watered:
                dryTime.StartClock();
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
            int timeElapsed = GameTimestamp.CompareTimestampInHours(dryTime, timestamp);
            if (timeElapsed > TimeWaterToExpireInHours)
            {
                SwitchLandStatus(LandStatus.Farmland);
                dryTime.StartClock();
            }
        }
    }

    public LandSaveData GetLandSaveData()
    {

        LandSaveData landSaveData = new();

        landSaveData.landStatus = landStatus;
        landSaveData.startWateredTime = dryTime.gameStartTime.ToString();
        return landSaveData;

    }
    public void SetLandFromSaveData(LandSaveData landSaveData)
    {
        dryTime.gameStartTime = DateTime.Parse(landSaveData.startWateredTime);
        dryTime.RestoreGameTime(dryTime.gameStartTime);
        SwitchLandStatus(landSaveData.landStatus);
    }

}
