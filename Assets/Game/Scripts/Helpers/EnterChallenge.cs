using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnterChallenge : MonoBehaviour
{
    public ButtonAction action;
    public bool inArea = false;
    public bool entrada = false;

    private InputActions inputActions;
    // public EnterChallengesManager enterChallengesManager;
    public bool isMatScene;
    public bool isPtScene;
    public bool isEnScene;
    public AudioClip enterChallengeSound;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    // private void Start()
    // {
    //     enterChallengesManager = FindObjectOfType<EnterChallengesManager>();
    // }

    private void Update()
    {
        var buttonPressed = inputActions.Game.Action.WasPerformedThisFrame();

        if (buttonPressed && inArea)
        {
            string sceneTarget = "";
            if (isPtScene)
            {
                sceneTarget = EnterChallengesManager.Instance.NextScenePT();

            }
            if (isMatScene)
            {
                sceneTarget = EnterChallengesManager.Instance.NextSceneMT();
            }
            UIController.Instance.PlayUIFx(enterChallengeSound);
            UIController.Instance.SetStandardButton();
            SceneTransitionManager.Instance.SalvarLocalizaçaoNaCena(entrada);
            WorldLandSaveManager.Instance.LandManagerSaveDataToJson();
            StartCoroutine(EnterChallengesManager.Instance.GoToScene(sceneTarget));

        }

    }

    void OnTriggerEnter(Collider ColliderPlayer)
    {
        if (ColliderPlayer.CompareTag("Player"))
        {
            inArea = true;
            UIController.Instance.ActiveButton(action);
        }

    }

    void OnTriggerExit(Collider other)
    {
        inArea = false;
        UIController.Instance.SetStandardButton();
    }

}
