using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
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
