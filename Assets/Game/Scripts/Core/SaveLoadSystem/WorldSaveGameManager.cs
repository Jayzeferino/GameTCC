using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSaveGameManager : MonoBehaviour
{

    public static WorldSaveGameManager instance;
    [SerializeField] PlayerController player;

    [Header("Save Data Writer")]
    SaveDataWitter saveDataWitter;

    [Header("Currrent Character Data")]
    public CharacterSaveData currentCharacterSaveData;
    [SerializeField] private string filename;

    [Header("SAVE / LOAD")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (saveGame)
        {
            saveGame = false;
            SaveGame();
        }
        else if (loadGame)
        {
            loadGame = false;
        }

    }

    public void SaveGame()
    {
        saveDataWitter = new SaveDataWitter();
        saveDataWitter.saveDataDirectoryPath = Application.persistentDataPath;
        saveDataWitter.dataSaveFileName = filename;

        player.SaveCharacterDataToCurrentSaveData(ref currentCharacterSaveData);

        saveDataWitter.WriteCharacterDataToSaveFile(currentCharacterSaveData);
        Debug.Log("SAVING GAME ... ");
        Debug.Log("SAVED IN: " + filename);
        Debug.Log(Application.persistentDataPath);
    }
}


