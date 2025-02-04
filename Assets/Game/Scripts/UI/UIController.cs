using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private InputActions inputActions;
    public GameObject inventoryScreen;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
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
