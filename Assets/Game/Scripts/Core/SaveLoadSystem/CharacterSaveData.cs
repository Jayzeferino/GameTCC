using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterSaveData
{
    public string characterName;
    public double wallet;

    [Header("Stamina")]
    public float maxStamina;
    // public float currentStamina;

    [Header("SkillLevels")]
    public float mathLv;
    public float portLv;

    [Header("WorldCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    public List<InventoryItemDict> invetoryItems;
    public List<InventoryItemDict> tabBarItems;

    [Header("Portal Stats")]
    public PortalsStatsSaveData portalsSaveData;

    [Header("Lands")]
    public List<LandManagerSaveData> landSaveData;



}
