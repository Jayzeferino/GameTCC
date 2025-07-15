using UnityEngine;

public class SceneManagerMap : MonoBehaviour
{
    private void Awake()
    {
        UIController.Instance.StopAllSounds();
        UIController.Instance.PlayMainMapMusics();
    }
    private void Start()
    {
        SceneTransitionManager.Instance.CarregarLocalizaçaoNaCena();
        WorldSaveGameManager.instance.SetLandInWorld();
        FindObjectOfType<DisableUIForMiniGames>().SetActive();
    }
}