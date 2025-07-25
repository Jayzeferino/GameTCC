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
    private RoundRobinWeighted PTroundRobin;
    private RoundRobinWeighted MTroundRobin;
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

    }

    private void Start()
    {
        PTroundRobin = new RoundRobinWeighted(PTsceneListWeighted);
        MTroundRobin = new RoundRobinWeighted(MTsceneListWeighted);
        currentPTScene = PTroundRobin.GetAtualItem();
        currentMTScene = MTroundRobin.GetAtualItem();

    }


    public string NextScenePT()
    {
        return PTroundRobin.Next();
    }

    public void UpdatePriorityPT()
    {
        PTroundRobin.ExecUpdateWeightList(currentPTScene);
    }
    public string NextSceneMT()
    {
        return MTroundRobin.Next();
    }

    public void UpdatePriorityMT()
    {
        MTroundRobin.ExecUpdateWeightList(currentMTScene);
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

        PTroundRobin = new RoundRobinWeighted(portalsStatsSaveData.PTsceneListWeighted);
        MTroundRobin = new RoundRobinWeighted(portalsStatsSaveData.MTsceneListWeighted);
        currentPTScene = portalsStatsSaveData.currentPTScene;
        currentMTScene = portalsStatsSaveData.currentMTScene;

    }
}


class WeightedNode
{
    public string Name;
    public int Weight;
    public int CurrentWeight;

    public WeightedNode(string name, int weight)
    {
        Name = name;
        Weight = weight;
        CurrentWeight = 0;
    }
}