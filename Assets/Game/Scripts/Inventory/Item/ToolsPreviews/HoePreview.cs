
using UnityEngine;

[CreateAssetMenu(menuName = "ToolPreviews/hoe preview")]
public class ToolPreview : IToolPreview
{

    [Header("Tool Interaction ModelPreview")]

    public PreviewModel previewInterationItem;
    public GameObject instantiateObject;
    public GameObject objectPreview;
    public Material originalMaterial;
    public Material previewMaterial;
    public Vector3 groundPosition;
    public Renderer renderer;


    public override void Show(RaycastHit hit)
    {

        if (objectPreview == null)
        {
            SetInteractionModelPreview();
        }

        ShowPreview(hit);

    }

    private void ShowPreview(RaycastHit hit)
    {

        var groundPosition = hit.point;
        if (hit.collider.CompareTag("Ground"))
        {
            objectPreview.transform.position = groundPosition;
        }
    }

    private void SetInteractionModelPreview()
    {

        originalMaterial = previewInterationItem.intantiateMaterial;
        previewMaterial = previewInterationItem.previewMaterial;
        objectPreview = Instantiate(previewInterationItem.modelPrefab, Vector3.zero, Quaternion.identity);
        objectPreview.gameObject.tag = "ObjPreview";
        renderer = objectPreview.GetComponent<Renderer>();
        renderer.material = previewMaterial;
        RayManager.Instance.isBuilding = true;
    }
}