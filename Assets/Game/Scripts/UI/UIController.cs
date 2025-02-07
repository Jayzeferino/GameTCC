using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private InputActions inputActions;
    public GameObject inventoryScreen;
    public GameObject itemSlots;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        itemSlots = inventoryScreen.transform.GetChild(0).GetChild(0).gameObject;
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



}
