
using UnityEngine;

[CreateAssetMenu(menuName = "ToolActions/watering action")]
public class WateringAction : IItemAction
{
    public LandItem landItem;
    public override void Execute(RaycastHit hit)
    {
        if (hit.collider.CompareTag("FarmLand"))
        {
            LandManager landManager = hit.transform.gameObject.GetComponent<LandManager>();
            landManager.WaterFarmLand();
        }

    }
}