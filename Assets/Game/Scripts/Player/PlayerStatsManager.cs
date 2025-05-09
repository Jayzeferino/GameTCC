using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public string characterName;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;

    [Header("SkillLevels")]
    public int mathLv;
    public int portLv;

    [Header("WorldCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;


}
