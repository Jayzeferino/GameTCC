using System.Collections.Generic;

[System.Serializable]

public class PortalsStatsSaveData
{
    public List<SceneData> PTsceneListWeighted;
    public List<SceneData> MTsceneListWeighted;
    public string currentPTScene;
    public string currentMTScene;

    public PortalsStatsSaveData(List<SceneData> PtList, List<SceneData> MtList, string currentPt, string currentMt)
    {
        PTsceneListWeighted = PtList;
        MTsceneListWeighted = MtList;
        currentMTScene = currentMt;
        currentPTScene = currentPt;
    }

}