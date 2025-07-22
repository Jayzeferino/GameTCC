using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolInteractor : MonoBehaviour
{
    PlayerAnimatorController playerAnimator;
    PlayerSoundManager playerSoundManager;


    private void Awake()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimatorController>();
        playerSoundManager = GetComponentInChildren<PlayerSoundManager>();

    }


    public void HandleToolInteraction(InvetoryItem toolItem)
    {
        playerAnimator.animator.SetBool("usingTool", true);
        if (toolItem.buttonAction.actionFx != null)
        {
            playerSoundManager.SetActionSound(toolItem.buttonAction.actionFx);
        }

        playerAnimator.PlayTargetAnimator(toolItem.ACTION_TOOL, true);
    }

    public void HandleItemInteraction(InvetoryItem item)
    {
        playerAnimator.animator.SetBool("usingTool", true);
        playerAnimator.PlayTargetAnimator(item.ACTION_TOOL, true);
        if (item.buttonAction.actionFx != null)
        {
            playerSoundManager.SetActionSound(item.buttonAction.actionFx);
        }
    }


}
