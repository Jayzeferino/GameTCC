using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class EnterChallengesManager : MonoBehaviour
{
    public static EnterChallengesManager Instance;

    [Header("UI da Tela de Loading")]
    public GameObject _loadingScreenPanel; // Arraste seu LoadingScreenPanel para cá no Inspector
    public TextMeshProUGUI _loadingText;
    private SmoothRoundRobinWeighted PTroundRobin;
    private SmoothRoundRobinWeighted MTroundRobin;
    public List<SceneData> PTsceneListWeighted;
    public List<SceneData> MTsceneListWeighted;
    private string currentPTScene;
    private string currentMTScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PTroundRobin = new SmoothRoundRobinWeighted();
        MTroundRobin = new SmoothRoundRobinWeighted();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("last_save"))
        {
            PTroundRobin.AddFromList(PTsceneListWeighted);
            MTroundRobin.AddFromList(MTsceneListWeighted);
        }

    }
    // Exemplo de como você chamaria isso de outro script ou de um botão na UI
    // void Update()
    // {
    //     // Apenas para teste, remova no jogo final
    //     if (Input.GetKeyDown(KeyCode.M))
    //     {
    //         currentPTScene = NextScenePT(); // Simula seu incremento original
    //         PTsceneListWeighted = PTroundRobin.GetSceneData();

    //         Debug.Log("Cena PT: " + currentPTScene);
    //     }
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         currentMTScene = NextSceneMT(); // Simula seu incremento original
    //         MTsceneListWeighted = MTroundRobin.GetSceneData();
    //         Debug.Log("Cena MT: " + currentMTScene);
    //     }
    // }

    public string NextScenePT()
    {
        currentPTScene = PTroundRobin.GetNext();
        PTsceneListWeighted = PTroundRobin.GetSceneData();
        return currentPTScene;


    }

    public string NextSceneMT()
    {
        currentMTScene = MTroundRobin.GetNext();
        MTsceneListWeighted = MTroundRobin.GetSceneData();
        return currentMTScene;
    }
    public void UpdatePriorityPT()
    {
        PTroundRobin.SetWeight(currentPTScene, 4);
        MTsceneListWeighted = MTroundRobin.GetSceneData();

    }

    public void UpdatePriorityMT()
    {
        MTroundRobin.SetWeight(currentMTScene, 4);
        MTsceneListWeighted = MTroundRobin.GetSceneData();
    }

    public IEnumerator GoToScene(string sceneName)
    {

        if (_loadingScreenPanel != null)
        {
            _loadingScreenPanel.SetActive(true);
            // Opcional: Reinicia a barra de progresso e o texto
            if (_loadingText != null) _loadingText.text = "0%";
        }
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            if (_loadingText != null)
            {
                _loadingText.text = $"{Mathf.RoundToInt(progress * 100)}%";
            }

            if (progress >= 0.9f)
            {
                _loadingScreenPanel.SetActive(false);
            }
            yield return null;
        }

    }

    public PortalsStatsSaveData GetChallengesStats()
    {
        PortalsStatsSaveData stats = new(PTsceneListWeighted, MTsceneListWeighted, currentPTScene, currentMTScene);
        return stats;
    }

    public void UpdateChallengerStatsFromSaveFile(PortalsStatsSaveData portalsStatsSaveData)
    {

        PTroundRobin.AddFromList(portalsStatsSaveData.PTsceneListWeighted);
        MTroundRobin.AddFromList(portalsStatsSaveData.MTsceneListWeighted);
        currentPTScene = portalsStatsSaveData.currentPTScene;
        currentMTScene = portalsStatsSaveData.currentMTScene;

    }
}