using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnterChallenge : MonoBehaviour
{
    public List<SceneData> scenesList;
    private EnterChallengesManager enterChallengesManager;
    private void Start()
    {
        enterChallengesManager = GetComponent<EnterChallengesManager>();
        enterChallengesManager.Init(scenesList);

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            string sceneTarget = enterChallengesManager.NextScene();
            SceneManager.LoadScene(sceneTarget);
        }
    }



}
