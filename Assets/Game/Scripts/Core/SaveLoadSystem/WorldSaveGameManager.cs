using System.Collections;
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
            LoadGame();
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

    public void LoadGame()
    {
        saveDataWitter = new SaveDataWitter();
        saveDataWitter.saveDataDirectoryPath = Application.persistentDataPath;
        saveDataWitter.dataSaveFileName = filename;

        currentCharacterSaveData = saveDataWitter.LoadCharacterSaveDataFromJson();

        StartCoroutine(LoadWorldSceneAsynchronously());
    }

    private IEnumerator LoadWorldSceneAsynchronously()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();

        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(0);

        while (!loadOperation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            yield return null;
        }

        player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);
    }
}


