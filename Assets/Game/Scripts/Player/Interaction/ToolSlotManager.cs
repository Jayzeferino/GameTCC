using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSlotManager : MonoBehaviour
{
    ToolHolderSlot leftHandSlot;
    ToolHolderSlot rightHandSlot;
    RayInteraction rayInteraction;


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


    public void LoadToolOnSlot(ToolItem toolItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadToolModel(toolItem);

        }
        else
        {
            rightHandSlot.LoadToolModel(toolItem);
            StartInteractorRayTool();
        }
    }

    public void StartInteractorRayTool()
    {

        if (rightHandSlot != null && rightHandSlot.currentToolItem.hasInteractor)
        {
            rayInteraction.SetInteractionModelPreview(rightHandSlot.currentToolItem.previewInterationItem);
        }

    }

}
