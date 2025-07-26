using System;

[Serializable]
public class SceneData
{
    public string cena;
    public int weight;
    public int CurrentWeight;

    public SceneData(string cena, int weight)
    {
        this.cena = cena;
        this.weight = weight;
        this.CurrentWeight = 0;
    }
}