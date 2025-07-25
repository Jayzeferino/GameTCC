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

  }
  private void Start()
  {
    WorldSaveGameManager.instance.player = this;
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
    currentCharacterSaveData.characterName = playerStatsManager.characterName;
    currentCharacterSaveData.xPosition = characterMovement.transform.position.x;
    currentCharacterSaveData.yPosition = characterMovement.transform.position.y;
    currentCharacterSaveData.zPosition = characterMovement.transform.position.z;
    currentCharacterSaveData.wallet = playerStatsManager.wallet;
    currentCharacterSaveData.mathLv = playerStatsManager.mathLv;
    currentCharacterSaveData.portLv = playerStatsManager.portLv;
    currentCharacterSaveData.portLv = playerStatsManager.portLv;
    currentCharacterSaveData.mathPoints = playerStatsManager.mathPoints;
    currentCharacterSaveData.portPoints = playerStatsManager.portPoints;
    currentCharacterSaveData.maxStamina = playerStatsManager.maxStamina;
    // currentCharacterSaveData.currentStamina = playerStatsManager.currentStamina;
    currentCharacterSaveData.portalsSaveData = EnterChallengesManager.Instance.GetChallengesStats();
    currentCharacterSaveData.invetoryItems = playerInventory.SlotItemsInventoryToSavaData();
    currentCharacterSaveData.tabBarItems = playerInventory.ToolBoxItemsInventoryToSavaData();
    currentCharacterSaveData.landSaveData = WorldLandSaveManager.Instance.GetLandManagerSaveDataList();
  }
  public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterSaveData)
  {
    playerStatsManager.characterName = currentCharacterSaveData.characterName;
    playerStatsManager.wallet = currentCharacterSaveData.wallet;
    playerStatsManager.mathLv = currentCharacterSaveData.mathLv;
    playerStatsManager.portLv = currentCharacterSaveData.portLv;
    playerStatsManager.mathPoints = currentCharacterSaveData.mathPoints;
    playerStatsManager.portPoints = currentCharacterSaveData.portPoints;
    playerStatsManager.maxStamina = currentCharacterSaveData.maxStamina;
    playerStatsManager.xPosition = currentCharacterSaveData.xPosition;
    playerStatsManager.yPosition = currentCharacterSaveData.yPosition;
    playerStatsManager.zPosition = currentCharacterSaveData.zPosition;
    playerInventory.InventorySlotListToSavaData(currentCharacterSaveData.invetoryItems);
    playerInventory.InventoryTabBarListToSavaData(currentCharacterSaveData.tabBarItems);
    WorldLandSaveManager.Instance.InstanciateAndLoadLandManagerSaveDataList(currentCharacterSaveData.landSaveData);
    EnterChallengesManager.Instance.UpdateChallengerStatsFromSaveFile(currentCharacterSaveData.portalsSaveData);
    characterMovement.SetNewPosition(new Vector3(currentCharacterSaveData.xPosition, currentCharacterSaveData.yPosition, currentCharacterSaveData.zPosition));
  }
}
