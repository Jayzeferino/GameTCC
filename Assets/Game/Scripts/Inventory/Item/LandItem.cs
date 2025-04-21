using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Land Item")]
public class LandItem : Item
{
    public List<GameObject> ModelPlantPhases;

    public int minutesToGrow;

}
