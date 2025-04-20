using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  private InputManager inputManager;

  [SerializeField] private CharacterMovement characterMovement;
  [SerializeField] private Animator animator;
  [SerializeField] private PlayerAnimatorController animatorController;
  [Header("Player Flags")]
  public bool isInteracting;

  private void Start()
  {
    inputManager = GetComponent<InputManager>();
  }

  private void Update()
  {
    isInteracting = animator.GetBool("isInteracting");
    inputManager.HandleAllInputs();

  }

  private void LateUpdate()
  {
    characterMovement.isJumping = animator.GetBool("isJumping");
    animator.SetBool("isGrounded", characterMovement.isGrounded);
  }


  public void SaveCharacterDataToCurrentSaveData(ref CharacterSaveData currentCharacterSaveData)
  {
    currentCharacterSaveData.xPosition = transform.position.x;
    currentCharacterSaveData.yPosition = transform.position.y;
    currentCharacterSaveData.zPosition = transform.position.z;
  }

}
