
using UnityEngine;

[CreateAssetMenu(menuName = "ToolActions/fork action")]
public class ForkAction : IItemAction
{
    public override void Execute(RaycastHit hit)
    {
        if (hit.collider.CompareTag("FarmLand"))
        {
            WorldLandSaveManager.Instance.DeleteLandFromManager(hit.collider.transform.root.GetComponent<LandManager>().landId);
            Destroy(hit.collider.transform.root.gameObject);
            Debug.Log("Deleting Forking landID: " + hit.collider.transform.root.GetComponent<LandManager>().landId);
        }
    }
}
