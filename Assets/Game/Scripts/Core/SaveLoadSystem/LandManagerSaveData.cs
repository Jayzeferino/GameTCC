using System;
using UnityEngine;

[System.Serializable]
public class LandManagerSaveData
{
    [Header("LandManager")]
    public int landId;
    public bool hasPlant;
    public int grow;
    public string startGrowTime;
    public int landItemId;

    [Header("LandCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    public LandSaveData landSaveData;

}
