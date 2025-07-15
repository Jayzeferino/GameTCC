using UnityEngine;
using UnityEngine.SceneManagement;

public class MathLvUpCollect : MonoBehaviour
{
    public ButtonAction action;
    public bool inArea = false;
    private InputActions inputActions;
    private PlayerStatsManager playerStats;
    public AudioClip mathLvUpSound;



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
            playerStats.SetMathLv();
            gameObject.SetActive(false);
            UIController.Instance.SetStandardButton();
            UIController.Instance.PlayUIFx(mathLvUpSound);
            SceneManager.LoadScene("MainMap");
        }

    }

    void OnTriggerEnter(Collider ColliderPlayer)
    {
        if (ColliderPlayer.CompareTag("Player"))
        {
            inArea = true;
            UIController.Instance.ActiveButton(action);
            playerStats = ColliderPlayer.gameObject.GetComponentInParent<PlayerStatsManager>();
            UIController.Instance.SetStandardButton();
        }

    }

    void OnTriggerExit(Collider other)
    {
        inArea = false;
        UIController.Instance.SetStandardButton();
    }



}
