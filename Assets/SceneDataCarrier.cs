using UnityEngine;

public class SceneDataCarrier : MonoBehaviour
{

    public bool fromMenu = false;
    public bool newGame = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
