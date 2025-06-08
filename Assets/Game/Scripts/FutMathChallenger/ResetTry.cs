using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTry : MonoBehaviour
{
    public Transform initPosition;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameEventManager.instance.ResetMathExpressions(true);
            collider.GetComponent<CharacterMovement>().SetNewPosition(initPosition.position);
        }
    }
}
