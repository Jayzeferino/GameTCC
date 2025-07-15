using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playFxOnStepFloor : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }

    }

}
