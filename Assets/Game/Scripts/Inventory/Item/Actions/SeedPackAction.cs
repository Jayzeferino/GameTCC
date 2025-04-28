
using UnityEngine;

[CreateAssetMenu(menuName = "ToolActions/seed Pack action")]
public class SeedPackAction : IItemAction
{
    public LandItem landItem;
    public override void Execute(RaycastHit hit)
    {
        if (hit.transform.gameObject != null && hit.transform.CompareTag("FarmLand"))
        {
            LandManager landManager = hit.transform.gameObject.GetComponent<LandManager>();
            landManager.LoadPlantOnSlot(landItem);
        }
    }
}
