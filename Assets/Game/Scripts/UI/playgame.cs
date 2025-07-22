using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGameController : MonoBehaviour
{
    public bool hasLoadedGame = false;
    AudioSource audioSource;
    SceneDataCarrier sceneDataCarrier;
    public string cenaParaCarregar = "MainMap";

    private void Awake()
    {
        sceneDataCarrier = FindAnyObjectByType<SceneDataCarrier>();
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("last_save"))
        {
            hasLoadedGame = true;
        }
        else
        {
            hasLoadedGame = false;
        }
        sceneDataCarrier.fromMenu = true;
    }
    private void Start()
    {
        if (hasLoadedGame)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void StartNewGame()
    {

        if (hasLoadedGame)
        {
            LoadGame();
        }
        else
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        sceneDataCarrier.newGame = true;
        StartCoroutine(EsperarAudioECarregarCena());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }

    public void LoadGame()
    {
        sceneDataCarrier.newGame = false;
        StartCoroutine(EsperarAudioECarregarCena());
    }


    private IEnumerator EsperarAudioECarregarCena()
    {
        if (audioSource != null)
        {
            audioSource.Play();

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }

        SceneManager.LoadScene(cenaParaCarregar);
    }

}
