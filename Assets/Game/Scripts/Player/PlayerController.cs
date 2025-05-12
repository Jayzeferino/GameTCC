using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  private InputManager inputManager;
  private PlayerStatsManager playerStatsManager;
  private PlayerInventory playerInventory;
  [SerializeField] private CharacterMovement characterMovement;
  [SerializeField] private Animator animator;
  [SerializeField] private PlayerAnimatorController animatorController;

  [Header("Player Flags")]
  public bool isInteracting;

  private void Awake()
  {
    playerStatsManager = GetComponent<PlayerStatsManager>();
    playerInventory = GetComponent<PlayerInventory>();
    WorldSaveGameManager.instance.player = this;
  }
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
    currentCharacterSaveData.invetoryItems = playerInventory.SlotItemsInventoryToSavaData();
    currentCharacterSaveData.tabBarItems = playerInventory.ToolBoxItemsInventoryToSavaData();
    currentCharacterSaveData.landSaveData = WorldLandSaveManager.Instance.GetLandManagerSaveDataList();

  }

  public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterSaveData)
  {
    playerStatsManager.characterName = currentCharacterSaveData.characterName;
    playerStatsManager.mathLv = currentCharacterSaveData.mathLv;
    playerStatsManager.portLv = currentCharacterSaveData.portLv;
    transform.position = new Vector3(currentCharacterSaveData.xPosition, currentCharacterSaveData.yPosition, currentCharacterSaveData.zPosition);
    playerInventory.InventorySlotListToSavaData(currentCharacterSaveData.invetoryItems);
    playerInventory.InventoryTabBarListToSavaData(currentCharacterSaveData.tabBarItems);
    WorldLandSaveManager.Instance.InstanciateAndLoadLandManagerSaveDataList(currentCharacterSaveData.landSaveData);
  }

}
