using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarChave : MonoBehaviour
{
    [SerializeField] GameObject porta;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Vector3 currentGatePosition = porta.transform.position;
            porta.transform.position = Vector3.MoveTowards(currentGatePosition, new Vector3(currentGatePosition.x, currentGatePosition.y + 5, currentGatePosition.z), 1 * Time.deltaTime);

            // Vector3.Slerp(porta.transform.position, new Vector3(porta.transform.position.x, 8, porta.transform.position.z), 30 * Time.deltaTime);
            gameObject.SetActive(false);
        }
    }
}
