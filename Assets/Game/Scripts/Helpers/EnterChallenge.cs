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
    public List<SceneData> scenesList;
    private EnterChallengesManager enterChallengesManager;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Start()
    {
        enterChallengesManager = GetComponent<EnterChallengesManager>();
        enterChallengesManager.Init(scenesList);
    }

    private void Update()
    {
        var buttonPressed = inputActions.Game.Action.WasPerformedThisFrame();

        if (buttonPressed && inArea)
        {
            SceneTransitionManager.Instance.SalvarLocalizaçaoNaCena(entrada);
            string sceneTarget = enterChallengesManager.NextScene();
            SceneManager.LoadScene(sceneTarget);
            UIController.Instance.SetStandardButton();

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
