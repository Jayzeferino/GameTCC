
using UnityEngine;

[CreateAssetMenu(menuName = "ToolActions/fork action")]
public class ForkAction : IItemAction
{
    public override void Execute(RaycastHit hit)
    {
        if (hit.collider.CompareTag("FarmLand"))
        {
            Destroy(hit.collider.transform.root.gameObject);
        }
    }
}
