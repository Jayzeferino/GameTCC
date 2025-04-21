using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Tool Item")]
public class ToolItem : InvetoryItem
{
    [Header("Tool Interaction ModelPreview")]
    public bool hasPreview;
    // public PreviewModel previewInterationItem;
    public IToolPreview toolPreview;

    public override void UseItem()
    {
        this.itemAction?.Execute();
    }
    public void ShowPreviewTool(RaycastHit hit)
    {
        if (hasPreview)
        {
            toolPreview.Show(hit);
        }

    }

}

public class UsePotion : IItemAction
{
    public void Execute()
    {
        Debug.Log("Curando o jogador...");
    }
}

public class EquipSword : IItemAction
{
    public void Execute()
    {
        Debug.Log("Espada equipada!");
    }
}

