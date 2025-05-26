using UnityEngine;

public class SceneManagerMap : MonoBehaviour
{
    private void Start()
    {
        SceneTransitionManager.Instance.CarregarLocalizaçaoNaCena();
    }
}