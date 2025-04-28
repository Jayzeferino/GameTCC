
using UnityEngine;

public class RayManager : MonoBehaviour
{

    public static RayManager Instance { get; set; }
    public MarkerInteraction marker;
    public bool isBuilding;
    public RaycastHit actualHit;
    public GameObject ourInteractable;
    public bool onTarget = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        isBuilding = false;
        marker = GetComponent<MarkerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        ActualHit();
        onTarget = marker.onTarget;
        marker.isBuilding = isBuilding;
    }

    public void ActualHit()
    {
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);

            ourInteractable = hit.transform.gameObject;

            actualHit = hit;

            marker.ShowSelectIcon(hit);
        }
        else
        {
            onTarget = false;
            isBuilding = false;
        }
    }

    public RaycastHit GetHit()
    {
        return actualHit;
    }
    public bool RayOnTarget()
    {
        return onTarget;
    }

    public void DoToolPreview(ToolItem item)
    {

        item.ShowPreviewTool(actualHit);
    }

    public void StopToolPreview(ToolItem item)
    {

        item.StopShowPreviewTool(actualHit);


    }
    public void DoToolAction(ToolItem item)
    {
        item.UseItem(actualHit);
    }
}
