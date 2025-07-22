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
    private EnterChallengesManager enterChallengesManager;
    public bool isMatScene;
    public bool isPtScene;
    public bool isEnScene;
    public AudioClip enterChallengeSound;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Start()
    {
        enterChallengesManager = FindObjectOfType<EnterChallengesManager>();
    }

    private void Update()
    {
        var buttonPressed = inputActions.Game.Action.WasPerformedThisFrame();

        if (buttonPressed && inArea)
        {
            string sceneTarget = "";
            if (isPtScene)
            {
                sceneTarget = enterChallengesManager.NextScenePT();

            }
            if (isMatScene)
            {
                sceneTarget = enterChallengesManager.NextSceneMT();
            }
            UIController.Instance.PlayUIFx(enterChallengeSound);
            UIController.Instance.SetStandardButton();
            SceneTransitionManager.Instance.SalvarLocalizaçaoNaCena(entrada);
            WorldLandSaveManager.Instance.LandManagerSaveDataToJson();
            StartCoroutine(enterChallengesManager.GoToScene(sceneTarget));

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
