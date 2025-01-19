using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlotManager : MonoBehaviour
{
    ToolHolderSlot leftHandSlot;
    ToolHolderSlot rightHandSlot;
    RayInteraction rayInteraction;
    private InputActions inputActions;


    private void Awake()
    {
        rayInteraction = GetComponentInChildren<RayInteraction>();
        rayInteraction = rayInteraction.GetComponent<RayInteraction>();

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
        var actionInput = inputActions.Game.Action.WasPressedThisFrame();
        if (actionInput)
        {
            if (rightHandSlot != null && rightHandSlot.currentToolItem.isTool)
            {
                if (rightHandSlot.currentToolItem.name == "Hoe")
                {
                    HoeAction();
                }

                if (rightHandSlot.currentToolItem.name == "Regador")
                {
                    WateringAction();
                }
            }

            if (rightHandSlot != null && rightHandSlot.currentToolItem.isHavestItem)
            {
                PlantAction();

            }
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
            SetPreviewToolInteract();
        }
    }

    public void SetPreviewToolInteract()
    {

        if (rightHandSlot != null && rightHandSlot.currentToolItem.isTool && rightHandSlot.currentToolItem.hasInteractor)
        {
            rayInteraction.SetInteractionModelPreview(rightHandSlot.currentToolItem.previewInterationItem);
        }
    }

    private void PlantAction()
    {
        if (rightHandSlot.currentToolItem.isHavestItem)
        {
            rayInteraction.SetPlantInFarmLand(rightHandSlot.currentToolItem.landItem);
        }
    }
    private void HoeAction()
    {
        if (rightHandSlot.currentToolItem.hasInteractor)
        {
            rayInteraction.PlacePreview();
        }
    }
    private void WateringAction()
    {
        rayInteraction.WaterFarmLand();
    }
}
