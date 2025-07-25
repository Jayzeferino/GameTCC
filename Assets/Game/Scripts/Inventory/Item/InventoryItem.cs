using UnityEngine;

public abstract class InvetoryItem : Item
{
    public GameObject modelPrefab;

    public double price;
    public float priceLoss = 0.6f;
    public bool uniqueUse;

    [Header("Tool Interaction Animation")]
    public string ACTION_TOOL;

    public ButtonAction buttonAction;
    //public AudioClip actionSound;
    public abstract void UseItem(RaycastHit hit);

}