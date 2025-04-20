using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterSaveData
{
    public string characterName;
    public int mathLv;
    public int portLv;

    [Header("WorldCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;


}
