using System.Collections.Generic;
using UnityEngine;

public class EnterChallengesManager : MonoBehaviour
{

    private RoundRobinWeighted roundRobin;
    private List<SceneData> sceneListWeighted;
    private string currentScene;

    public void Init(List<SceneData> sceneList)
    {
        sceneListWeighted = sceneList;
        roundRobin = new RoundRobinWeighted(sceneListWeighted);
        currentScene = roundRobin.GetAtualItem();
    }

    void Start()
    {
        sceneListWeighted = new();
    }

    public string NextScene()
    {
        return roundRobin.Next();
    }

    public void UpdatePriority()
    {
        roundRobin.ExecUpdateWeightList(currentScene);
    }

}
