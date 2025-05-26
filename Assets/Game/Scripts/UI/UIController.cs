using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; set; }
    private InputActions inputActions;
    public GameObject shopUI;
    private Sprite standardButtonIcon;
    public GameObject actionButtonBase;
    private GameObject actionButtonIcon;
    public GameObject inventoryScreen;
    public GameObject toolBox;
    public GameObject itemSlots;
    public GameObject itemBox;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();

        if (inventoryScreen != null && toolBox != null)
        {
            itemSlots = inventoryScreen.transform.GetChild(0).GetChild(0).gameObject;
            itemBox = toolBox.transform.GetChild(0).gameObject;

        }

        if (actionButtonBase != null)
        {
            standardButtonIcon = actionButtonBase.transform.GetChild(0).GetComponent<Image>().sprite;
            actionButtonIcon = actionButtonBase.transform.GetChild(0).gameObject;
        }

    }

    private void Update()
    {
        OpenInventory();
    }

    private void OpenInventory()
    {
        var OpenInventory = inputActions.Game.Inventory.WasPerformedThisFrame();

        if (OpenInventory)
        {
            inventoryScreen.SetActive(!inventoryScreen.activeInHierarchy);
        }


    }


    public void OpenShopUI()
    {
        shopUI.SetActive(true);
    }

    public void ActiveButton(ButtonAction action)
    {
        Image buttonImg = actionButtonBase.GetComponent<Image>();
        Color color = buttonImg.color;
        color.a = 0.85f; // 30% de opacidade
        buttonImg.color = color;
        actionButtonIcon.GetComponent<Image>().sprite = action.actionIcon;
        color = actionButtonIcon.GetComponent<Image>().color;
        color.a = 1f;
        actionButtonIcon.GetComponent<Image>().color = color;
    }

    public void SetStandardButton()
    {
        Image buttonImg = actionButtonBase.GetComponent<Image>();
        Color color = buttonImg.color;
        color.a = 0.3f; // 30% de opacidade
        buttonImg.color = color;
        actionButtonIcon.GetComponent<Image>().sprite = standardButtonIcon;
        color = actionButtonIcon.GetComponent<Image>().color;
        color.a = 0f;
        actionButtonIcon.GetComponent<Image>().color = color;
    }

}
