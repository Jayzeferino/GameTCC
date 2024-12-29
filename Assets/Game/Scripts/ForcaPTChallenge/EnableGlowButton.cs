using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGlowButton : MonoBehaviour
{
    [SerializeField] private int buttonId;
    private Material material;
    private void Start()
    {
        GameEventManager.instance.OnButtonChangeColorHandler += ChangeButtonEmissionColor;
        material = gameObject.GetComponent<Renderer>().material;
    }
    private void ChangeButtonEmissionColor(bool sucess, int id)
    {

        if (buttonId == id)
        {
            if (sucess)
            {
                material.EnableKeyword("_EMISSION");

            }
            else
            {
                material.color = Color.red;
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", Color.red);
            }
        }

    }
}
