using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Tool Item")]
public class ToolItem : InvetoryItem
{
    [Header("Tool Interaction ModelPreview")]
    public bool hasPreview;
    public IToolPreview toolPreview;

    public override void UseItem(RaycastHit hit)
    {

        if (itemAction)
        {
            itemAction.Execute(hit);
        }

    }

    public void ShowPreviewTool(RaycastHit hit)
    {
        if (hasPreview)
        {
            toolPreview.Show(hit);
        }

    }

    public void StopShowPreviewTool(RaycastHit hit)
    {
        if (hasPreview)
        {
            toolPreview.StopShowPreview(hit);
        }

    }

}