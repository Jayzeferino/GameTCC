using TMPro;
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

    public TextMeshProUGUI staminaUI;
    public TextMeshProUGUI walletUI;
    public TextMeshProUGUI walletInfo;
    public TextMeshProUGUI mathLvInfo;
    public TextMeshProUGUI portLvInfo;

    [Header("PlayerStats")]
    public PlayerStatsManager statsManager;
    public AudioSource audioSource;

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

        statsManager = PlayerStatsManager.Instance;
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
        InfosUIUpdate();
    }

    private void InfosUIUpdate()
    {
        staminaUI.text = statsManager.currentStamina.ToString();
        walletUI.text = statsManager.wallet.ToString();
        walletInfo.text = statsManager.wallet.ToString();
        mathLvInfo.text = statsManager.mathLv.ToString();
        portLvInfo.text = statsManager.portLv.ToString();
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
        color.a = 0f; // 1% de opacidade
        buttonImg.color = color;
        actionButtonIcon.GetComponent<Image>().sprite = standardButtonIcon;
        color = actionButtonIcon.GetComponent<Image>().color;
        color.a = 0f;
        actionButtonIcon.GetComponent<Image>().color = color;
    }

    public void PlayUIFx(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void PlayMapMusic(int musicIndex)
    {
        GetComponent<GameMusicManager>().PlaybackMiniGameMusic(musicIndex);
    }

    public void PlayMainMapMusics()
    {
        GetComponent<GameMusicManager>().PlayMainMapMusics();
    }
    public void StopAllSounds()
    {
        GetComponent<GameMusicManager>().StopAllSounds();
    }


}
