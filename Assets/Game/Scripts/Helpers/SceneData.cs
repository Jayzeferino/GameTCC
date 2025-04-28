using System;

[Serializable]
public class SceneData
{
    public string item;
    public int weight;

    public SceneData(string item, int weight)
    {
        this.item = item;
        // weight = weight;
    }
}