using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; set; }
    ToolSlotManager toolSlotManager;
    UIController uIController;
    public List<GameObject> slotList = new();
    public List<GameObject> tabBarList = new();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isFull;

    public ToolItem rightHandToolItem;
    public ToolItem leftHandToolItem;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;

        }

        toolSlotManager = GetComponent<ToolSlotManager>();

        uIController = GetComponent<UIController>();


    }

    private void Start()
    {
        if (uIController)
        {
            PopulateSlotList();
        }
    }

    private void Update()
    {

        if (tabBarList.Count() > 0 && tabBarList[0].GetComponent<ItemSlot>().Item && rightHandToolItem == null)
        {
            rightHandToolItem = (ToolItem)tabBarList[0].GetComponentInChildren<ItemInSlot>().itemInSlot;
            toolSlotManager.LoadToolOnSlot(rightHandToolItem, false);
        }

        if (tabBarList[0].GetComponent<ItemSlot>().Item == null)
        {
            toolSlotManager.UnloadRightToolSlot();
            rightHandToolItem = null;
        }

    }

    private void PopulateSlotList()
    {
        foreach (Transform child in uIController.itemSlots.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }

        foreach (Transform child in uIController.itemBox.transform)
        {
            if (child.CompareTag("Slot"))
            {
                tabBarList.Add(child.gameObject);
            }
        }

    }

    public void RemoveFromInventory(int itemID)
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.GetComponent<ItemSlot>().Item != null && slot.GetComponentInChildren<ItemInSlot>().itemInSlot.itemID == itemID)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
                return;
            }

        }

        foreach (GameObject slot in tabBarList)
        {
            if (slot.GetComponent<ItemSlot>().Item != null && slot.GetComponentInChildren<ItemInSlot>().itemInSlot.itemID == itemID)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
                return;
            }
        }

    }

    public void AddToInvetory(InvetoryItem item)
    {
        if (CheckFull())
        {
            Debug.Log("invetory is full");
        }
        else
        {
            FindNextEmptySlot();
            itemToAdd = Instantiate(Resources.Load<GameObject>("UiPrefabs/ItemImage"), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);
            itemToAdd.GetComponentInChildren<ItemInSlot>().itemInSlot = item;
            itemToAdd.GetComponent<Image>().sprite = item.itemIcon;
        }
    }

    public void AddToInvetoryFromSaveFile(InvetoryItem item, int slot)
    {
        whatSlotToEquip = slotList[slot];
        itemToAdd = Instantiate(Resources.Load<GameObject>("UiPrefabs/ItemImage"), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemToAdd.GetComponentInChildren<ItemInSlot>().itemInSlot = item;
        itemToAdd.GetComponent<Image>().sprite = item.itemIcon;
    }
    public void AddToTabItemFromSaveFile(InvetoryItem item, int slot)
    {
        whatSlotToEquip = tabBarList[slot];
        itemToAdd = Instantiate(Resources.Load<GameObject>("UiPrefabs/ItemImage"), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemToAdd.GetComponentInChildren<ItemInSlot>().itemInSlot = item;
        itemToAdd.GetComponent<Image>().sprite = item.itemIcon;
    }

    private bool CheckFull()
    {
        return isFull;
    }

    private void FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.GetComponent<ItemSlot>().Item == null)
            {
                isFull = false;
                whatSlotToEquip = slot;
                return;
            }
            isFull = true;
        }
    }

    public List<InventoryItemDict> SlotItemsInventoryToSavaData()
    {

        List<InventoryItemDict> itemsIDs = new();
        int index = 0;
        foreach (var item in slotList)
        {
            ItemInSlot slot = item.GetComponentInChildren<ItemInSlot>();
            if (slot != null)
            {
                itemsIDs.Add(new InventoryItemDict(index, slot.itemInSlot.itemID));
            }

            index++;
        }
        return itemsIDs;
    }
    public List<InventoryItemDict> ToolBoxItemsInventoryToSavaData()
    {
        List<InventoryItemDict> itemsIDs = new();
        int index = 0;
        foreach (var item in tabBarList)
        {
            ItemInSlot slot = item.GetComponentInChildren<ItemInSlot>();
            if (slot != null)
            {
                itemsIDs.Add(new InventoryItemDict(index, slot.itemInSlot.itemID));
            }

            index++;
        }
        return itemsIDs;
    }

    public void InventorySlotListToSavaData(List<InventoryItemDict> inventario)
    {
        foreach (InventoryItemDict item in inventario)
        {
            InvetoryItem inventoryItem = WorldItemDatabase.Instance.GetInvetoryItem(item.itemID);
            AddToInvetoryFromSaveFile(inventoryItem, item.slot);
        }
    }
    public void InventoryTabBarListToSavaData(List<InventoryItemDict> inventario)
    {
        foreach (InventoryItemDict item in inventario)
        {
            InvetoryItem inventoryItem = WorldItemDatabase.Instance.GetInvetoryItem(item.itemID);
            AddToTabItemFromSaveFile(inventoryItem, item.slot);
        }
    }


    public List<InvetoryItem> GetInventoryitemsFromInventory()
    {
        List<InvetoryItem> items = new();

        foreach (GameObject slot in slotList)
        {
            if (slot.GetComponent<ItemSlot>().Item != null)
            {
                items.Add(slot.GetComponentInChildren<ItemInSlot>().itemInSlot);
            }

        }

        foreach (GameObject slot in tabBarList)
        {
            if (slot.GetComponent<ItemSlot>().Item != null)
            {
                items.Add(slot.GetComponentInChildren<ItemInSlot>().itemInSlot);

            }
        }

        return items;
    }
}

