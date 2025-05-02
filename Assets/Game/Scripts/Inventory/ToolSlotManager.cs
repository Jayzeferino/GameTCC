using UnityEngine;

public class ToolSlotManager : MonoBehaviour
{
    ToolHolderSlot leftHandSlot;
    ToolHolderSlot rightHandSlot;
    bool rightHandIsEquiped = false;
    RayManager rayManager;
    private PlayerToolInteractor playerToolInteractor;

    private InputActions inputActions;


    private void Awake()
    {
        rayManager = GetComponentInChildren<RayManager>();
        playerToolInteractor = GetComponent<PlayerToolInteractor>();

        ToolHolderSlot[] toolHolderslots = GetComponentsInChildren<ToolHolderSlot>();
        foreach (ToolHolderSlot toolSlot in toolHolderslots)
        {
            if (toolSlot.isLeftHandSlot)
            {
                leftHandSlot = toolSlot;
            }
            else if (toolSlot.isRightHandSlot)
            {
                rightHandSlot = toolSlot;
            }
        }

    }
    private void Start()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    private void Update()
    {
        if (rightHandIsEquiped && rightHandSlot.currentToolItem.hasPreview)
        {
            rayManager.DoToolPreview(rightHandSlot.currentToolItem);
        }
    }

    public void LoadToolOnSlot(ToolItem toolItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadToolModel(toolItem);

        }
        else
        {
            rightHandSlot.LoadToolModel(toolItem);
            rightHandIsEquiped = true;
        }
    }
    public void UnloadRightToolSlot()
    {
        rightHandSlot.UnloadToolAndDestroy();

        if (rightHandSlot.currentToolItem != null && rightHandSlot.currentToolItem.hasPreview)
        {
            rayManager.StopToolPreview(rightHandSlot.currentToolItem);
            RayManager.Instance.isBuilding = false;
            rightHandIsEquiped = false;
        }

    }
    public void UnloadLeftToolSlot()
    {
        leftHandSlot.UnloadToolAndDestroy();
        rightHandIsEquiped = false;
    }

}
