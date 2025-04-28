
using UnityEngine;

[CreateAssetMenu(menuName = "ToolActions/hoe action")]
public class HoeAction : IItemAction
{
    public PreviewModel previewInterationItem;
    private Renderer renderer;

    public override void Execute(RaycastHit hit)
    {
        if (hit.collider.CompareTag("ObjPreview"))
        {
            GameObject land = Instantiate(previewInterationItem.modelPrefab, hit.collider.transform.position, Quaternion.identity);
            renderer = land.GetComponent<Renderer>();
            renderer.material = previewInterationItem.intantiateMaterial;
            hit.collider.transform.position = land.transform.position + new Vector3(5f, 5f, 0);
        }
    }
}
