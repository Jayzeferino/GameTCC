using System;
using System.Collections.Generic;

class RoundRobinWeighted
{
    private List<SceneData> items;
    private int currentIndex;
    private int currentWeight;
    private int maxWeight;
    private int gcdWeight;

    public RoundRobinWeighted(List<SceneData> items)
    {
        this.items = items;
        UpdateWeights();
    }

    private void UpdateWeights()
    {
        maxWeight = GetMaxWeight();
        gcdWeight = GetGCDWeight();
    }
    public List<SceneData> GetSceneDataRound()
    {
        return items;
    }
    private int GetMaxWeight()
    {
        int max = 0;
        foreach (var item in items)
        {
            if (item.weight > max)
                max = item.weight;
        }
        return max;
    }

    private int GetGCDWeight()
    {
        int gcd = items[0].weight;
        foreach (var item in items)
        {
            gcd = GCD(gcd, item.weight);
        }
        return gcd;
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private void UpdateItemWeight(string item, int newWeight)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i] = new SceneData(item, newWeight);
                UpdateWeights();
                return;
            }
        }
        throw new ArgumentException("Item não encontrado na lista");
    }

    public void ExecUpdateWeightList(string item)
    {
        var newWeight = items[currentIndex].weight;
        UpdateItemWeight(item, newWeight++);
    }

    public string GetAtualItem()
    {
        return items[currentIndex].item;
    }

    public string Next()
    {
        while (true)
        {
            currentIndex = (currentIndex + 1) % items.Count;
            if (currentIndex == 0)
            {
                currentWeight -= gcdWeight;
                if (currentWeight <= 0)
                {
                    currentWeight = maxWeight;
                    if (currentWeight == 0)
                        return null;
                }
            }
            if (items[currentIndex].weight >= currentWeight)
                return items[currentIndex].item;
        }
    }
}
