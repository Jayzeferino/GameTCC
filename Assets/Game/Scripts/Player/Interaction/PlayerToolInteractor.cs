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


    public void HandleToolInteraction(InvetoryItem toolItem)
    {
        playerAnimator.animator.SetBool("usingTool", true);
        playerAnimator.PlayTargetAnimator(toolItem.ACTION_TOOL, true);
    }

    public void HandleItemInteraction(InvetoryItem item)
    {
        playerAnimator.animator.SetBool("usingTool", true);
        playerAnimator.PlayTargetAnimator(item.ACTION_TOOL, true);
    }


}
