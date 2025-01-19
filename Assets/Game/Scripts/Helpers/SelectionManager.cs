using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance { get; set; }
    public bool onTarget;
    [SerializeField] private GameObject actionButton;
    GameObject selectionIcon;
    GameObject selectionIconInstantiate;
    private void Start()
    {
        selectionIcon = Resources.Load<GameObject>("UiPrefabs/Sinal");
        selectionIconInstantiate = Instantiate(selectionIcon, new Vector3(0, -50, 0), Quaternion.Euler(180, 0, 0));
        selectionIconInstantiate.SetActive(false);

    }
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
    }
    void Update()
    {

        Vector3 origin = transform.position;
        if (Physics.SphereCast(origin, 0.5f, transform.forward, out RaycastHit hit, 3f))
        {
            var selectionTransform = hit.transform;

            InteractableObject ourInteractable = selectionTransform.GetComponent<InteractableObject>();

            if (ourInteractable)
            {
                selectionIconInstantiate = Instantiate(selectionIcon, ourInteractable.transform.position + new Vector3(0, 2, 0), Quaternion.Euler(180, 0, 0));
                selectionIconInstantiate.transform.position = ourInteractable.transform.position + new Vector3(0, 1.4f, 0);
                selectionIconInstantiate.SetActive(true);
                var text = ourInteractable.GetItemName();
                // interactionInfoUi.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
                // interactionInfoUi.SetActive(true);
                EnableActionButton(1f);
            }
            else
            {
                selectionIconInstantiate.SetActive(false);
                EnableActionButton(0.5f);
                // interactionInfoUi.SetActive(false);

            }
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward * hit.distance), Color.red);

        }
        else
        {
            selectionIconInstantiate.SetActive(false);
            EnableActionButton(0.5f);
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward * 5f), Color.black);

        }

    }

    private void EnableActionButton(float cor)
    {
        Color currentColor = actionButton.transform.GetComponent<Image>().color;

        currentColor.a = cor;

        actionButton.transform.GetComponent<Image>().color = currentColor;

        currentColor = actionButton.transform.GetChild(0).GetComponent<Image>().color;

        currentColor.a = cor;

        actionButton.transform.GetChild(0).GetComponent<Image>().color = currentColor;
    }
}
