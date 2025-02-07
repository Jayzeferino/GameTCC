using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateKinematicOfBeforeFloors : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameEventManager.instance.CurrentFloorId(id);
        }
    }
}

