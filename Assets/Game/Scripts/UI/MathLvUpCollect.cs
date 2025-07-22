using UnityEngine;

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
            DificultyLvManager dificuldadeLvManager = FindAnyObjectByType<DificultyLvManager>();
            UIController.Instance.SetStandardButton();
            UIController.Instance.PlayUIFx(mathLvUpSound);
            playerStats.AddMathPoints(dificuldadeLvManager.GetPointsToGain());
            dificuldadeLvManager.IncreaseChallengeLevel();
            StartCoroutine(EnterChallengesManager.Instance.GoToScene("MainMap"));
            gameObject.SetActive(false);
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
