using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputActions inputActions;

    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerAnimatorController animatorController;
    private PlayerToolInteractor playerToolInteractor;
    private RayManager rayManager;
    private PlayerInventory playerInventory;

    private float moveAmount;

    [Header("Mobile")]
    [SerializeField] private bool useMobile;
    [SerializeField] private GameObject mobileCanvas;

    private void Awake()
    {
        playerToolInteractor = GetComponent<PlayerToolInteractor>();
        playerInventory = GetComponent<PlayerInventory>();
        rayManager = GetComponentInChildren<RayManager>();

    }
    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
        }
        inputActions.Enable();
    }
    public void HandleAllInputs()
    {
        HandleAction();
        HandleMovimentInput();

    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void OnValidate()
    {
        RefreshOnScreenControls();
    }
    private void RefreshOnScreenControls()
    {
        if (mobileCanvas)
        {
            mobileCanvas.SetActive(useMobile);
            Cursor.visible = useMobile;
        }

    }
    private void HandleMovimentInput()
    {
        // if (!animatorController.animator.GetBool("usingTool") || !animatorController.animator.GetBool("isInteracting"))
        //     return;

        var moveInput = inputActions.Game.Move.ReadValue<Vector2>();
        var wantJump = inputActions.Game.Jump.WasPerformedThisFrame();
        moveAmount = Mathf.Clamp01(Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y));
        animatorController.UpdateAnimatorValues(0, moveAmount);

        characterMovement.SetInput(new CharacterMovementInput()
        {
            MoveInput = moveInput,
            LookRotation = cameraController.LookRotation,
            WantsToJump = wantJump
        });

        var look = inputActions.Game.Look.ReadValue<Vector2>();

        cameraController.IncrementLookRotation(new Vector2(look.y, look.x));
    }

    private void HandleAction()
    {
        var wasAction = inputActions.Game.Action.WasPerformedThisFrame();
        if (wasAction)
        {
            if (RayManager.Instance.ourInteractable.CompareTag("InteractableItem"))
            {
                animatorController.animator.SetBool("usingTool", true);
                animatorController.PlayTargetAnimator("Interact", true);
            }

            if (playerInventory.rightHandToolItem != null && !rayManager.ourInteractable.CompareTag("InteractableItem"))
            {
                rayManager.DoToolAction(playerInventory.rightHandToolItem);
                playerToolInteractor.HandleToolInteraction(playerInventory.rightHandToolItem);

            }
        }
    }
}
