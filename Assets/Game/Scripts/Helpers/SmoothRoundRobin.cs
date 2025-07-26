using System;
using System.Collections.Generic;
using System.Linq;

class SmoothRoundRobinWeighted
{
    private List<SceneData> nodes = new();

    private int totalWeight;

    public void AddFromList(List<SceneData> sceneDataList)
    {
        foreach (var sceneData in sceneDataList)
        {
            AddNode(sceneData.cena, sceneData.weight);
        }
    }

    public void AddNode(string cena, int weight)
    {
        if (weight <= 0)
            throw new ArgumentException("Peso deve ser maior que 0");

        if (nodes.Any(n => n.cena == cena))
            throw new ArgumentException($"Node '{cena}' já existe.");

        nodes.Add(new SceneData(cena, weight));
        totalWeight += weight;
    }

    public string GetNext()
    {
        if (nodes.Count == 0)
            throw new InvalidOperationException("Nenhum nó disponível.");

        SceneData best = null;

        foreach (var node in nodes)
        {
            node.CurrentWeight += node.weight;

            if (best == null || node.CurrentWeight > best.CurrentWeight)
            {
                best = node;
            }
        }

        if (best == null)
            throw new InvalidOperationException("Nenhum nó selecionado. Verifique os pesos.");

        best.CurrentWeight -= totalWeight;

        return best.cena;
    }

    public void IncreaseWeight(string nodeName, int amount)
    {

        var node = nodes.FirstOrDefault(n => n.cena == nodeName);
        if (node == null)
        {
            throw new ArgumentException($"Node '{nodeName}' não encontrado.");
        }

        node.weight += amount;
        totalWeight += amount;
    }


    public void SetWeight(string cena, int newWeight)
    {
        var node = nodes.FirstOrDefault(n => n.cena == cena);
        if (node == null)
            throw new ArgumentException($"Node '{cena}' não encontrado.");

        totalWeight += newWeight - node.weight;
        node.weight = newWeight;
    }

    public List<SceneData> GetSceneData()
    {
        return nodes;
    }
}
