using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    public string characterName;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;

    [Header("SkillLevels")]
    public float mathLv;
    public float portLv;

    [Header("WorldCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    public List<InventoryItemDict> invetoryItems;
    public List<InventoryItemDict> tabBarItems;

    [Header("Lands")]
    public List<LandManagerSaveData> landSaveData;

}
