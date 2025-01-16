using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    ToolSlotManager toolSlotManager;
    public ToolItem rightHandTool;
    public ToolItem leftHandTool;

    private void Awake()
    {
        toolSlotManager = GetComponent<ToolSlotManager>();
    }

    private void Start()
    {
        toolSlotManager.LoadToolOnSlot(rightHandTool, false);
        toolSlotManager.LoadToolOnSlot(leftHandTool, true);
    }
}
