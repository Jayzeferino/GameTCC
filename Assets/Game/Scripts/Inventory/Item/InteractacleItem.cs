using UnityEngine;

public class InteractacleItem : MonoBehaviour

{

    private InputActions inputActions;
    public ToolItem item;
    public bool playerInRange;

    private void Start()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    public ToolItem GetItem()
    {
        return this.item;
    }

    private void Update()
    {
        var actionCollect = inputActions.Game.Action.WasPressedThisFrame();
        if (actionCollect && playerInRange && RayInteraction.instance.onTarget)
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
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
