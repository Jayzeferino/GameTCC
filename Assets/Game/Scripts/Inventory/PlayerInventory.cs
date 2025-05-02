using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    private bool CheckFull()
    {
        return false;
    }

    private void FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.GetComponent<ItemSlot>().Item == null)
            {

                whatSlotToEquip = slot;
                return;
            }

        }
    }
}
