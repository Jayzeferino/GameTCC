using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlotManager : MonoBehaviour
{
    ToolHolderSlot leftHandSlot;
    ToolHolderSlot rightHandSlot;
    RayManager rayManager;
    private InputActions inputActions;


    private void Awake()
    {
        rayManager = GetComponentInChildren<RayManager>();
        // rayInteraction = rayInteraction.GetComponent<RayInteraction>();

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

            if (rightHandSlot != null)
            {
                rightHandSlot.currentToolItem.UseItem();

            }
            // if (rightHandSlot != null && rightHandSlot.currentToolItem.type == Item.ItemType.Tool)
            // {
            //     if (rightHandSlot.currentToolItem.name == "Hoe")
            //     {
            //         HoeAction();
            //     }

            //     if (rightHandSlot.currentToolItem.name == "Regador")
            //     {
            //         WateringAction();
            //     }
            // }

            // if (rightHandSlot != null && rightHandSlot.currentToolItem.type == Item.ItemType.Seed)
            // {
            //     PlantAction();

            // }
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

            if (rightHandSlot.currentToolItem.hasPreview)
            {
                rayManager.DoToolPreview(rightHandSlot.currentToolItem);
            }

        }
    }
    public void UnloadRightToolSlot()
    {
        rightHandSlot.UnloadToolAndDestroy();

    }
    public void UnloadLeftToolSlot()
    {
        leftHandSlot.UnloadToolAndDestroy();

    }

    // public void SetPreviewToolInteract()
    // {

    //     if (rightHandSlot != null && rightHandSlot.currentToolItem.type == Item.ItemType.Tool && rightHandSlot.currentToolItem.hasInteractor)
    //     {
    //         rayInteraction.SetInteractionModelPreview(rightHandSlot.currentToolItem.previewInterationItem);
    //     }
    // }

    // private void PlantAction()
    // {
    //     if (rightHandSlot.currentToolItem.type == Item.ItemType.Seed)
    //     {
    //         rayInteraction.SetPlantInFarmLand(rightHandSlot.currentToolItem.landItem);
    //     }
    // }
    // private void HoeAction()
    // {
    //     if (rightHandSlot.currentToolItem.hasInteractor)
    //     {
    //         rayInteraction.PlacePreview();
    //     }
    // }
    // private void WateringAction()
    // {
    //     rayInteraction.WaterFarmLand();
    // }
}
