using UnityEngine;

public class SceneManagerMap : MonoBehaviour
{

    SceneDataCarrier sceneDataCarrier;
    public Transform playerSpawnPoint;

    private void Awake()
    {
        sceneDataCarrier = FindAnyObjectByType<SceneDataCarrier>();


    }
    private void Start()
    {

        if (sceneDataCarrier.fromMenu != true)
        {
            UIController.Instance.StopAllSounds();
        }

        UIController.Instance.PlayMainMapMusics();

        if (sceneDataCarrier.newGame && sceneDataCarrier.fromMenu)
        {
            sceneDataCarrier.fromMenu = false;
            SceneTransitionManager.Instance.LoadPlayerInMainMap(playerSpawnPoint.position);
        }
        else if (sceneDataCarrier.newGame == false && sceneDataCarrier.fromMenu)
        {
            Debug.Log("Loading existing game...");

            sceneDataCarrier.fromMenu = false;
            SceneTransitionManager.Instance.DeleteAllPlayerPrefsFromMap();
            WorldSaveGameManager.instance.LoadGame();

        }
        else
        {
            Debug.Log("Voltando para a cena principal...");
            SceneTransitionManager.Instance.CarregarLocalizaçaoNaCena();
            WorldLandSaveManager.Instance.InstanciateAndLoadLandManagerSaveDataList(WorldLandSaveManager.Instance.LoadLandData());

            GameObject loadingScreen = GameObject.Find("LoadingScreenPanel");
            if (loadingScreen != null)
            {
                loadingScreen.SetActive(false);
            }
        }

        FindObjectOfType<DisableUIForMiniGames>().SetActive();
    }
}