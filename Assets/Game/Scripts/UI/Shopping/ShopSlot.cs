using System;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI productName;
    [SerializeField] Sprite itemImage;
    public InvetoryItem item;
    public TextMeshProUGUI price;


    // Start is called before the first frame update
    public void BuyItem()
    {
        if (PlayerStatsManager.Instance.wallet >= item.price)
        {
            PlayerInventory.instance.AddToInvetory(this.item);
            PlayerStatsManager.Instance.wallet -= item.price;
        }

    }
    public void SellItem()
    {
        PlayerInventory.instance.RemoveFromInventory(item.itemID);

        if (item.price > 0)
        {
            PlayerStatsManager.Instance.wallet += Math.Round(item.price * item.priceLoss, 2);
        }
        Destroy(gameObject);
    }
}
