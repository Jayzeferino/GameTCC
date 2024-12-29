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
            collider.GetComponent<CharacterMovement>().SetNewPosition(new Vector3(-4f, 1.584463f, -39.71f));
        }
    }
}
