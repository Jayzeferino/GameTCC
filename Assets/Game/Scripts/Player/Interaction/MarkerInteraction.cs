
using UnityEngine;

public class MarkerInteraction : MonoBehaviour
{
    public GameObject selectionIcon;
    public GameObject selectionIconInstantiate;
    public bool isBuilding;
    public GameObject ourInteractable;
    public bool onTarget = false;

    void Awake()
    {
        ShowMarkerItemIteractor();
    }

    public void ShowMarkerItemIteractor()
    {
        selectionIcon = Resources.Load<GameObject>("UiPrefabs/Sinal");
        selectionIconInstantiate = Instantiate(selectionIcon, new Vector3(0, -50, 0), Quaternion.Euler(180, 0, 0));
        selectionIconInstantiate.SetActive(false);
    }

    public void ShowSelectIcon(RaycastHit hit)
    {
        var selectionTransform = hit.transform;

        ourInteractable = selectionTransform.gameObject;
        if (!isBuilding)
        {
            if (hit.transform.CompareTag("InteractableItem") && ourInteractable.GetComponent<InteractacleItem>().playerInRange)
            {
                onTarget = true;
                selectionIconInstantiate.SetActive(true);
                selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);
            }
            else if (hit.transform.CompareTag("FarmLand"))
            {
                onTarget = true;

                selectionIconInstantiate.SetActive(true);
                selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);

            }
            else
            {
                selectionIconInstantiate.SetActive(false);
                onTarget = false;
            }
        }
        else
        {
            selectionIconInstantiate.SetActive(false);
            onTarget = false;
        }
    }



}
