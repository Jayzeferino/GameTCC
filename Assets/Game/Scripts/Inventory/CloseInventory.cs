using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{

    public GameObject UI;
    // Start is called before the first frame update
    private void CloseUI()
    {
        UI.SetActive(false);
    }
}
