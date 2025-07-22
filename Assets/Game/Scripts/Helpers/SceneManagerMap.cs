using UnityEngine;

public class SceneManagerMap : MonoBehaviour
{

    SceneDataCarrier sceneDataCarrier;
    public Transform playerSpawnPoint;

    private void Awake()
    {
        UIController.Instance.StopAllSounds();
        UIController.Instance.PlayMainMapMusics();
        sceneDataCarrier = FindAnyObjectByType<SceneDataCarrier>();

    }
    private void Start()
    {
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
        }

        FindObjectOfType<DisableUIForMiniGames>().SetActive();
    }
}