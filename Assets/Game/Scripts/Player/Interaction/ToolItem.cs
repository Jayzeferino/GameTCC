using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Tool Item")]
public class ToolItem : Item
{
    public GameObject modelPrefab;

    public bool isUnarmed;
    public bool hasInteractor;

    [Header("Tool Interaction Animation")]
    public string ACTION_TOOL;

    [Header("Tool Interaction ModelPreview")]
    public PreviewInterationItem previewInterationItem;

}
