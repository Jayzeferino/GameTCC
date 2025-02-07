using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance { get; set; }
    ToolSlotManager toolSlotManager;
    UIController uIController;
    public List<GameObject> slotList = new();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isFull;

    public ToolItem rightHandTool;
    public ToolItem leftHandTool;

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
        PopulateSlotList();
        toolSlotManager.LoadToolOnSlot(rightHandTool, false);
        toolSlotManager.LoadToolOnSlot(leftHandTool, true);
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

    }

    public void AddToInvetory(ToolItem item)
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
