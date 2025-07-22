using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{

    public static WorldSaveGameManager instance;
    public PlayerController player;


    [Header("Save Data Writer")]
    SaveDataWitter saveDataWitter;

    [Header("Currrent Character Data")]
    public CharacterSaveData currentCharacterSaveData;
    [SerializeField] private string filename;

    [Header("SAVE / LOAD")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;
    [SerializeField] bool clear;


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
            LoadGame();
        }
        else if (clear)
        {
            clear = false;
            ClearPlayerPrefs();
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
        PlayerPrefs.SetInt("last_save", 1);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        saveDataWitter = new SaveDataWitter();
        saveDataWitter.saveDataDirectoryPath = Application.persistentDataPath;
        saveDataWitter.dataSaveFileName = filename;

        currentCharacterSaveData = saveDataWitter.LoadCharacterSaveDataFromJson();

        // StartCoroutine(LoadWorldSceneAsynchronously());
        LoadWorldScene();
    }

    private IEnumerator LoadWorldSceneAsynchronously()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();

        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(1);

        while (!loadOperation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            yield return null;
        }

        player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);
    }
    private void LoadWorldScene()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();

        }

        player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);
    }


    public void SetLandInWorld()
    {
        WorldLandSaveManager.Instance.InstanciateAndLoadLandManagerSaveDataList(currentCharacterSaveData.landSaveData);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared.");
    }
}


