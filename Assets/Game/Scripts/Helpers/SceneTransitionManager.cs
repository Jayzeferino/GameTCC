using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    void Awake()
    {
        // Verifica se já existe uma instância
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
