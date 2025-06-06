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
                // Calcula a direção do chute (do jogador para a bola, ou na direção que o jogador está olhando)
                // Vector3 direcaoChute = (bola.transform.position - transform.position).normalized;
                // direcaoChute.y = 0.001f; // Opcional: para garantir que o chute seja mais horizontal

                // Você pode usar a direção para frente do jogador para um chute mais preciso:
                Vector3 direcaoChute = playerTransform.forward;
                direcaoChute.y = 0.1f;

                // Aplica a força à bola
                rbBola.AddForce(direcaoChute * forcaChute, ForceMode.Impulse);

            }
            UIController.Instance.SetStandardButton();

        }
        // Opcional: Desenhar a distância máxima de chute no editor para visualização
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
