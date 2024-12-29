using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTry : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameEventManager.instance.ResetMathExpressions(true);
            // GameObject player = collider.gameObject;
            // GameObject novoPlayer = Instantiate(player, new Vector3(76, 0, 0), Quaternion.identity);
            // novoPlayer.name = "PlayerController";
            // Destroy(collider.gameObject);
        }
    }
}
