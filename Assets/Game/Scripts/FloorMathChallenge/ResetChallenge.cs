using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetChallenge : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameEventManager.instance.SetFailInCalc(true);
            collider.GetComponent<CharacterMovement>().SetNewPosition(new Vector3(21.8f, 13.76f, -20.81f));

        }
    }
}
