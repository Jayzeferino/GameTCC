using UnityEngine;

public class BallKick : MonoBehaviour
{
    public float forcaChute = 30f; // Força aplicada à bola
    public ButtonAction action;
    public bool inArea = false;
    private InputActions inputActions;
    private Transform playerTransform;
    private GameObject bola; // Referência à bola no cenário

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        bola = gameObject;
    }

    private void Update()
    {
        var buttonPressed = inputActions.Game.Action.WasPerformedThisFrame();

        if (buttonPressed && inArea && bola != null)
        {

            Rigidbody rbBola = bola.GetComponent<Rigidbody>();

            if (rbBola != null)
            {

                Vector3 direcaoChute = playerTransform.forward;
                direcaoChute.y = 0.1f;

                rbBola.AddForce(direcaoChute * forcaChute, ForceMode.Impulse);

            }
            UIController.Instance.SetStandardButton();
            UIController.Instance.PlayUIFx(action.actionFx);

        }
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player"))
        {
            playerTransform = player.GetComponent<CharacterMovement>().transform;
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
