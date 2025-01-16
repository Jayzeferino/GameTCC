
using UnityEngine;

public class RayInteraction : MonoBehaviour
{

    // public PreviewInterationItem previewInterationItem;
    public GameObject instantiateObject;
    public GameObject objectPreview;
    public Material originalMaterial;
    public Material previewMaterial;
    private Vector3 groundPosition;
    private Renderer renderer;
    private InputActions inputActions;
    public bool isBuilding;

    private void Start()
    {
        // instantiateObject = previewInterationItem.modelPrefab;
        // originalMaterial = previewInterationItem.intantiateMaterial;
        // previewMaterial = previewInterationItem.previewMaterial;
        // objectPreview = Instantiate(instantiateObject, Vector3.zero, Quaternion.identity);
        // objectPreview.gameObject.tag = "ObjPreview";
        isBuilding = false;
    }
    void Update()
    {

        if (isBuilding)
        {
            StartPreview();
        }

    }

    private void StartPreview()
    {

        // var actionInput = inputActions.Game.Action.WasPressedThisFrame();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            ShowPreview(hit);

            // if (actionInput)
            // {
            //     PlacePreview(hit);

            // }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
        }
    }

    void PlacePreview(RaycastHit hit)
    {

        if (hit.collider.CompareTag("ObjPreview") || hit.collider.CompareTag("Ground"))
        {
            GameObject land = Instantiate(instantiateObject, objectPreview.transform.position, Quaternion.identity);
            renderer = land.GetComponent<Renderer>();
            renderer.material = originalMaterial;

        }

    }

    void ShowPreview(RaycastHit hit)
    {
        groundPosition = hit.point;
        if (hit.collider.CompareTag("Ground"))
        {
            objectPreview.transform.position = groundPosition;
        }
    }

    public void SetInteractionModelPreview(PreviewInterationItem previewInterationItem)
    {
        originalMaterial = previewInterationItem.intantiateMaterial;
        previewMaterial = previewInterationItem.previewMaterial;
        objectPreview = Instantiate(previewInterationItem.modelPrefab, Vector3.zero, Quaternion.identity);
        objectPreview.gameObject.tag = "ObjPreview";
        isBuilding = true;
    }
}
