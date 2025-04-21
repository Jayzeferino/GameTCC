using UnityEngine;

public abstract class InvetoryItem : Item
{
    public GameObject modelPrefab;
    [Header("Tool Interaction Animation")]
    public string ACTION_TOOL;

    public abstract void UseItem();

    // [Header("Tool Interaction ModelPreview")]

    // public bool hasInteractor;
    // public PreviewInterationItem previewInterationItem;

    // [Header("Havest Item Land Item")]
    // public LandItem landItem;

}