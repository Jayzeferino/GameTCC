
using UnityEngine;

public class RayInteraction : MonoBehaviour
{

    [SerializeField] GameObject instantiateObject;
    private GameObject objectPreview;
    public Material originalMaterial;
    public Material previewMaterial;
    private Vector3 groundPosition;
    Renderer renderer;

    private InputActions inputActions;


    public bool isBuilding;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
    // Update is called once per frame

    private void Start()
    {
        objectPreview = Instantiate(instantiateObject, Vector3.zero, Quaternion.identity);
        objectPreview.gameObject.tag = "ObjPreview";

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

        var actionInput = inputActions.Game.Action.WasPressedThisFrame();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            ShowPreview(hit);

            if (actionInput)
            {
                PlacePreview(hit);

            }

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
}
