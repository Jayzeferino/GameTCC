using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingUIManager : MonoBehaviour
{
    public static ShoppingUIManager Instance;
    public List<InvetoryItem> InventoryItemsStock;
    public List<GameObject> ItemsToBuy;
    public List<GameObject> ItemsToSell;
    private GameObject newSlot;
    public GameObject buyContent;
    public GameObject sellContent;
    private bool sellShopOpened;
    private PlayerInventory playerInventory;
    private PlayerStatsManager playerStat;
    public Vector3 SlotScale;


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

        }

        sellShopOpened = false;

        ItemsToBuy = new();

        ItemsToSell = new();

        playerInventory = FindObjectOfType<PlayerInventory>();
        playerStat = FindObjectOfType<PlayerStatsManager>();

        foreach (InvetoryItem item in InventoryItemsStock)
        {
            if (item.mathLvRequired <= playerStat.mathLv && item.portLvRequired <= playerStat.portLv)
            {
                ItemsToBuy.Add(FillBuySlot(item, buyContent));
            }
        }
    }

    private void Update()
    {

        if (sellContent.activeInHierarchy == true && sellShopOpened == true)
        {
            List<InvetoryItem> inventoryItems = playerInventory.GetInventoryitemsFromInventory();

            foreach (InvetoryItem item in inventoryItems)
            {
                ItemsToSell.Add(FillSellSlot(item, sellContent));
            }
            sellShopOpened = false;
        }
    }


    private GameObject FillSellSlot(InvetoryItem item, GameObject content)
    {
        newSlot = Instantiate(Resources.Load<GameObject>("UiPrefabs/SellShopSlot"));
        newSlot.transform.SetParent(content.transform);
        newSlot.transform.localScale = SlotScale;
        newSlot.GetComponent<ShopSlot>().item = item;
        newSlot.GetComponentsInChildren<Image>()[1].sprite = item.itemIcon;
        newSlot.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        newSlot.GetComponent<ShopSlot>().price.text = Math.Round(item.price * item.priceLoss, 2).ToString();
        return newSlot;
    }

    private GameObject FillBuySlot(InvetoryItem item, GameObject content)
    {

        newSlot = Instantiate(Resources.Load<GameObject>("UiPrefabs/BuyShopSlot"));
        newSlot.transform.SetParent(content.transform);
        newSlot.transform.localScale = SlotScale;
        newSlot.GetComponent<ShopSlot>().item = item;
        newSlot.GetComponentsInChildren<Image>()[1].sprite = item.itemIcon;
        newSlot.GetComponentInChildren<TextMeshProUGUI>().text = item.itemName;
        newSlot.GetComponent<ShopSlot>().price.text = item.price.ToString();
        return newSlot;
    }


    public void SellButtonIsActive()
    {
        sellShopOpened = true;
    }
}

