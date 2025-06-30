using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;
    CharacterMovement player;
    private string atualNameScene;

    void Awake()
    {
        // Verifica se já existe uma instância
        if (Instance == null)
        {
            // PlayerPrefs.DeleteAll();
            DeleteAllPlayerPrefsFromMap();
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre cenas
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Já existe? destrói o duplicado!
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<CharacterMovement>();
        atualNameScene = SceneManager.GetActiveScene().name;
    }

    public void SalvarLocalizaçaoNaCena(bool entrada)
    {
        if (entrada == true)
        {
            Debug.Log("SALVANDO LOCALIZAÇÃO DA CENA " + atualNameScene + " .. .. .. ");
            PlayerPrefs.SetFloat(atualNameScene + "X", player.transform.position.x);
            PlayerPrefs.SetFloat(atualNameScene + "Y", player.transform.position.y);
            PlayerPrefs.SetFloat(atualNameScene + "Z", player.transform.position.z);
            PlayerPrefs.Save();

        }

    }
    public void CarregarLocalizaçaoNaCena()
    {
        if (PlayerPrefs.HasKey(atualNameScene + "X") && PlayerPrefs.HasKey(atualNameScene + "Y") && PlayerPrefs.HasKey(atualNameScene + "Z"))
        {
            Debug.Log("CARREGANDO LOCALIZAÇÃO DA CENA " + atualNameScene + " .. .. .. ");

            player.SetNewPosition(new Vector3(
                PlayerPrefs.GetFloat(atualNameScene + "X", -1),
                PlayerPrefs.GetFloat(atualNameScene + "Y", -1),
                PlayerPrefs.GetFloat(atualNameScene + "Z", -1)));
        }
    }

    private static void print()
    {
        Debug.Log("PlayerPrefs:");
        string[] keys = { "MainMapX", "MainMapY", "MainMapZ" }; // Substitua pelas suas chaves conhecidas

        foreach (string key in keys)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.Log($"{key} = {PlayerPrefs.GetFloat(key, -1)}");
                Debug.Log($"{key} = {PlayerPrefs.GetFloat(key, -1)}");
                Debug.Log($"{key} = {PlayerPrefs.GetFloat(key, -1f)}");
            }
            else
            {
                Debug.Log($"{key} não encontrado.");
            }
        }
    }

    public void DeleteAllPlayerPrefsFromMap()
    {
        string[] keys = { "MainMapX", "MainMapY", "MainMapZ" }; // Substitua pelas suas chaves conhecidas

        foreach (string key in keys)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
            }
        }
    }
}
