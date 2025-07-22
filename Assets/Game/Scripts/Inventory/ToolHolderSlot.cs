using UnityEngine;

public class ToolHolderSlot : MonoBehaviour
{
    public Transform parentOverride;
    public bool isLeftHandSlot;
    public bool isRightHandSlot;
    public ToolItem currentToolItem;
    private GameObject currentToolModel;

    public void UnloadTool()
    {
        if (currentToolModel != null)
        {
            currentToolModel.SetActive(false);
            UIController.Instance.SetStandardButton();
        }
    }

    public void UnloadToolAndDestroy()
    {
        if (currentToolModel != null)
        {
            Destroy(currentToolModel);

        }

    }
    public void LoadToolModel(ToolItem toolItem)
    {

        UnloadToolAndDestroy();

        if (toolItem == null)
        {
            UnloadTool();
            return;
        }

        GameObject model = Instantiate(toolItem.modelPrefab) as GameObject;
        if (model != null)
        {
            if (parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
            currentToolItem = toolItem;
        }

        currentToolModel = model;
    }

}
