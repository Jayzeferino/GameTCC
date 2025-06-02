using System.Collections.Generic;
using UnityEngine;

public class EnterChallengesManager : MonoBehaviour
{
    public static EnterChallengesManager Instance;
    private RoundRobinWeighted PTroundRobin;
    private RoundRobinWeighted MTroundRobin;
    public List<SceneData> PTsceneListWeighted;
    public List<SceneData> MTsceneListWeighted;
    private string currentPTScene;
    private string currentMTScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        PTroundRobin = new RoundRobinWeighted(PTsceneListWeighted);
        MTroundRobin = new RoundRobinWeighted(MTsceneListWeighted);
        currentPTScene = PTroundRobin.GetAtualItem();
        currentMTScene = MTroundRobin.GetAtualItem();

    }

    // public void Init(List<SceneData> sceneList)
    // {
    //     sceneListWeighted = sceneList;
    //     roundRobin = new RoundRobinWeighted(sceneListWeighted);
    //     currentScene = roundRobin.GetAtualItem();
    // }

    // void Start()
    // {
    //     sceneListWeighted = new();
    // }

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

}
