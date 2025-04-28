using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Comum Item")]
public class ComumItem : InvetoryItem
{
    public override void UseItem(RaycastHit hit)
    {
        throw new System.NotImplementedException();
    }
}