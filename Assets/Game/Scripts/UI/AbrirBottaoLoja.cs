using UnityEngine;

public class AbrirBottaoLoja : MonoBehaviour
{
    public ButtonAction action;
    public bool inArea = false;
    private InputActions inputActions;


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
            UIController.Instance.OpenShopUI();
        }

    }

    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
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
