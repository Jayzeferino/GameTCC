using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour
{
    public ButtonAction action;
    public bool inArea = false;
    public bool entrada = false;
    private InputActions inputActions;
    public string sceneTarget;
    public bool failOnExit = false;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Update()
    {
        var buttonPressed = inputActions.Game.Action.WasPerformedThisFrame();


        if (buttonPressed && inArea)
        {
            SceneTransitionManager.Instance.SalvarLocalizaçaoNaCena(entrada);
            if (entrada)
            {
                WorldLandSaveManager.Instance.LandManagerSaveDataToJson();
            }

            if (failOnExit)
            {
                EnterChallengesManager.Instance.UpdatePriorityPT();
            }
            SceneManager.LoadScene(sceneTarget);
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
