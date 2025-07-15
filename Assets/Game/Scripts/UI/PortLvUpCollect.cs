using UnityEngine;
using UnityEngine.SceneManagement;

public class PortLvUpCollect : MonoBehaviour
{
    public ButtonAction action;
    public bool inArea = false;
    private InputActions inputActions;
    private PlayerStatsManager playerStats;
    public AudioClip portLvUpSound;


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
            playerStats.SetPortLv();
            gameObject.SetActive(false);
            UIController.Instance.SetStandardButton();
            UIController.Instance.PlayUIFx(portLvUpSound);
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

        }

    }

    void OnTriggerExit(Collider other)
    {
        inArea = false;
        UIController.Instance.SetStandardButton();
    }



}
