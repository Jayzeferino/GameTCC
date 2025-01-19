
using UnityEngine;

public class RayInteraction : MonoBehaviour
{


    public GameObject instantiateObject;
    public GameObject objectPreview;
    public Material originalMaterial;
    public Material previewMaterial;
    private Vector3 groundPosition;
    private Renderer renderer;
    private GameObject selectionIcon;
    private GameObject selectionIconInstantiate;
    private GameObject ourInteractable;
    private RaycastHit hit;

    public bool isBuilding;

    private void Start()
    {
        selectionIcon = Resources.Load<GameObject>("UiPrefabs/Sinal");
        selectionIconInstantiate = Instantiate(selectionIcon, new Vector3(0, -50, 0), Quaternion.Euler(180, 0, 0));
        selectionIconInstantiate.SetActive(false);
        isBuilding = false;
    }
    void Update()
    {
        StartPreview();
    }

    private void StartPreview()
    {

        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 10f))
        {
            if (isBuilding)
            {
                ShowPreview(hit);
            }

            var selectionTransform = hit.transform;

            ourInteractable = selectionTransform.gameObject;

            if (hit.transform.CompareTag("FarmLand"))
            {

                selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);
                selectionIconInstantiate.SetActive(true);

            }
            else
            {
                selectionIconInstantiate.SetActive(false);


            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
        }
        else
        {
            selectionIconInstantiate.SetActive(false);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
        }
    }

    public void PlacePreview()
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
        renderer = objectPreview.GetComponent<Renderer>();
        renderer.material = previewMaterial;
        objectPreview.gameObject.tag = "ObjPreview";
        isBuilding = true;
    }

    public void SetPlantInFarmLand(LandItem landItem)
    {
        if (hit.transform.gameObject != null)
        {
            LandManager landManager = hit.transform.gameObject.GetComponent<LandManager>();
            if (landManager.hasPlant == false)
            {
                landManager.LoadPlantOnSlot(landItem);
            }
        }
    }
}
