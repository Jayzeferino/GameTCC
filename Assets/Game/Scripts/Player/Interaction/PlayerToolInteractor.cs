using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolInteractor : MonoBehaviour
{
    PlayerAnimatorController playerAnimator;


    private void Awake()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorController>();

    }


    public void HandleToolInteraction(ToolItem toolItem)
    {
        playerAnimator.animator.SetBool("usingTool", true);
        playerAnimator.PlayTargetAnimator(toolItem.ACTION_TOOL, true);
    }
}
