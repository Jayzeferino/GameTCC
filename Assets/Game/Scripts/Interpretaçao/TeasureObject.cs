using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeasureObject : MonoBehaviour
{

    public int id;
    public Sprite treasureIcon;
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            GameEventManager.instance.OnCollectTreasure(treasureIcon, id, gameObject);
        }
    }
}
