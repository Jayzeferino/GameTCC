
using UnityEngine;

public class RayInteraction : MonoBehaviour
{

    public RayManager rayManager;
    // public GameObject instantiateObject;
    // public GameObject objectPreview;
    // public Material originalMaterial;
    // public Material previewMaterial;
    // private Vector3 groundPosition;
    // private Renderer renderer;
    // private GameObject selectionIcon;
    // private GameObject selectionIconInstantiate;
    // public GameObject ourInteractable;
    public RaycastHit actualHit;
    // public bool isBuilding;

    private void Awake()
    {

        rayManager = new();
    }

    // private void Start()
    // {
    //     rayManager.ShowMarkerItemIteractor();
    // }
    void Update()
    {

        ActualHit();
        //StartPreview();
    }
    // private void StartPreview()
    // {

    //     if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 10f))
    //     {
    //         if (isBuilding)
    //         {
    //             ShowPreview(hit);
    //         }

    //         ShowSelectIcon();

    //         Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
    //     }
    //     else
    //     {
    //         selectionIconInstantiate.SetActive(false);
    //         Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
    //     }
    // }


    public void ActualHit()
    {
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
            this.actualHit = hit;
        }
    }


    // private void ShowSelectIcon()
    // {
    //     var selectionTransform = hit.transform;

    //     ourInteractable = selectionTransform.gameObject;

    //     if (!isBuilding)
    //     {
    //         if (hit.transform.CompareTag("InteractableItem") && ourInteractable.GetComponent<InteractacleItem>().playerInRange)
    //         {
    //             onTarget = true;
    //             selectionIconInstantiate.SetActive(true);
    //             selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);
    //         }
    //         else if (hit.transform.CompareTag("FarmLand"))
    //         {
    //             onTarget = true;

    //             selectionIconInstantiate.SetActive(true);
    //             selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);

    //         }
    //         else
    //         {
    //             selectionIconInstantiate.SetActive(false);
    //             onTarget = false;
    //         }
    //     }
    //     else
    //     {
    //         selectionIconInstantiate.SetActive(false);
    //         onTarget = false;
    //     }
    // }

    // public void PlacePreview()
    // {

    //     if (hit.collider.CompareTag("ObjPreview") || hit.collider.CompareTag("Ground"))
    //     {
    //         GameObject land = Instantiate(instantiateObject, objectPreview.transform.position, Quaternion.identity);
    //         renderer = land.GetComponent<Renderer>();
    //         renderer.material = originalMaterial;
    //     }
    // }

    // void ShowPreview(RaycastHit hit)
    // {
    //     groundPosition = hit.point;
    //     if (hit.collider.CompareTag("Ground"))
    //     {
    //         objectPreview.transform.position = groundPosition;
    //     }
    // }

    // public void SetInteractionModelPreview(PreviewModel previewInterationItem)
    // {
    //     originalMaterial = previewInterationItem.intantiateMaterial;
    //     previewMaterial = previewInterationItem.previewMaterial;
    //     objectPreview = Instantiate(previewInterationItem.modelPrefab, Vector3.zero, Quaternion.identity);
    //     renderer = objectPreview.GetComponent<Renderer>();
    //     renderer.material = previewMaterial;
    //     objectPreview.gameObject.tag = "ObjPreview";
    //     isBuilding = true;
    // }

    // public void SetPlantInFarmLand(LandItem landItem)
    // {
    //     if (hit.transform.gameObject != null && hit.transform.CompareTag("FarmLand"))
    //     {
    //         LandManager landManager = hit.transform.gameObject.GetComponent<LandManager>();
    //         landManager.LoadPlantOnSlot(landItem);

    //     }
    // }

    // public void WaterFarmLand()
    // {
    //     LandManager landManager = hit.transform.gameObject.GetComponent<LandManager>();
    //     landManager.WaterFarmLand();
    // }
}
