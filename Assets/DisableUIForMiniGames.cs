using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUIForMiniGames : MonoBehaviour
{
    public GameObject InventoryButton;
    public GameObject InventoryUi;
    public GameObject WalletUi;
    public GameObject EnergyUI;
    public void SetActive()
    {
        InventoryButton.SetActive(true);
        InventoryUi.SetActive(true);
        WalletUi.SetActive(true);
        EnergyUI.SetActive(true);
    }
    public void SetUnactive()
    {
        InventoryButton.SetActive(false);
        InventoryUi.SetActive(false);
        WalletUi.SetActive(false);
        EnergyUI.SetActive(false);
    }
}
