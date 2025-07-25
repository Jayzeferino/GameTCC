using UnityEngine;

public class SceneDataCarrier : MonoBehaviour
{
    public static SceneDataCarrier Instance { get; set; }
    public bool fromMenu = false;
    public bool newGame = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Já existe? destrói o duplicado!
        }
    }

}
