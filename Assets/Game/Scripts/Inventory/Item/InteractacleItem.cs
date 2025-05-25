using UnityEngine;

public class InteractacleItem : MonoBehaviour

{

    private InputActions inputActions;
    public InvetoryItem item;
    public ButtonAction buttonAction;

    public bool playerInRange;

    private void Start()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    public InvetoryItem GetItem()
    {
        return this.item;
    }

    private void Update()
    {
        var actionCollect = inputActions.Game.Action.WasPressedThisFrame();
        if (actionCollect && playerInRange && RayManager.Instance.RayOnTarget())
        {
            PlayerInventory.instance.AddToInvetory(this.item);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            UIController.Instance.ActiveButton(buttonAction);
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            UIController.Instance.SetStandardButton();
        }
    }

}
