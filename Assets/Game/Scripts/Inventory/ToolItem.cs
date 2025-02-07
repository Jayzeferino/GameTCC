using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Tool Item")]
public class ToolItem : Item
{
    public GameObject modelPrefab;
    [Header("Tool Interaction Animation")]
    public string ACTION_TOOL;

    [Header("Tool Interaction ModelPreview")]
    public bool hasInteractor;
    public PreviewInterationItem previewInterationItem;

    [Header("Havest Item Land Item")]
    public LandItem landItem;



}
