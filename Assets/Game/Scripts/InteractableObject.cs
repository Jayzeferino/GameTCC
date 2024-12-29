using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange = false;


    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Update()
    {
        var actionInput = inputActions.Game.Action.WasPressedThisFrame();

        if (actionInput && playerInRange)
        {

            gameObject.SetActive(false);
        }
    }

    public string GetItemName()
    {
        return ItemName;
    }


    private void OnTriggerEnter(Collider other)
    {

        playerInRange = true;

    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
    }


}