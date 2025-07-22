
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
        }
        else
        {
            Instance = this;

        }
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
    Vector3 halfExtents = new Vector3(0.25f, 0.1f, 0.5f); // Metade do tamanho da caixa

    public void ActualHit()
    {
        // if (Physics.SphereCast(transform.position, 0.2f, Vector3.down, out RaycastHit hit, 10f))
        if (Physics.BoxCast(transform.position, halfExtents, Vector3.down, out RaycastHit hit, Quaternion.identity, 10f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down * hit.distance), Color.red);
            DrawBox(transform.position + Vector3.down * hit.distance, halfExtents, Quaternion.identity, Color.green);

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

        if (!UIController.Instance.uiButtonPressed)
        {
            UIController.Instance.ActiveButtonInTool(item.buttonAction);
        }
    }
    public void StopToolPreview(ToolItem item)
    {
        if (item != null && ourInteractable.CompareTag("ObjPreview"))
        {
            item.StopShowPreviewTool(actualHit);
            UIController.Instance.SetStandardButton();
        }
    }
    public void DoToolAction(ToolItem item)
    {
        item.UseItem(actualHit);

        if (item.uniqueUse && onTarget)
        {
            PlayerInventory.instance.RemoveFromInventory(item.itemID);
            UIController.Instance.SetStandardButton();

        }
    }

    void DrawBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        Vector3[] corners = new Vector3[8];

        Vector3 right = orientation * Vector3.right * halfExtents.x;
        Vector3 up = orientation * Vector3.up * halfExtents.y;
        Vector3 forward = orientation * Vector3.forward * halfExtents.z;

        // Calcular os 8 cantos da caixa
        corners[0] = center + right + up + forward;
        corners[1] = center + right + up - forward;
        corners[2] = center + right - up + forward;
        corners[3] = center + right - up - forward;
        corners[4] = center - right + up + forward;
        corners[5] = center - right + up - forward;
        corners[6] = center - right - up + forward;
        corners[7] = center - right - up - forward;

        // Desenhar arestas da caixa
        Debug.DrawLine(corners[0], corners[1], color);
        Debug.DrawLine(corners[0], corners[2], color);
        Debug.DrawLine(corners[0], corners[4], color);
        Debug.DrawLine(corners[1], corners[3], color);
        Debug.DrawLine(corners[1], corners[5], color);
        Debug.DrawLine(corners[2], corners[3], color);
        Debug.DrawLine(corners[2], corners[6], color);
        Debug.DrawLine(corners[3], corners[7], color);
        Debug.DrawLine(corners[4], corners[5], color);
        Debug.DrawLine(corners[4], corners[6], color);
        Debug.DrawLine(corners[5], corners[7], color);
        Debug.DrawLine(corners[6], corners[7], color);
    }

}
