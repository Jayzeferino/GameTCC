using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{


    private Renderer render;
    public float valorMolhado = 1.09f;
    public float valorSoloSaudavel = 0.70f;

    public enum LandStatus
    {
        Soil, Farmland, Watered
    }

    public LandStatus landStatus;

    private void Start()
    {
        render = gameObject.GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Farmland);

    }

    public void SwitchLandStatus(LandStatus statusSwitch)
    {
        landStatus = statusSwitch;
        switch (statusSwitch)
        {
            case LandStatus.Soil:
                ChangeMaterialColor(valorSoloSaudavel);
                break;
            case LandStatus.Farmland:
                ChangeMaterialColor(valorSoloSaudavel);
                break;
            case LandStatus.Watered:
                ChangeMaterialColor(valorMolhado);
                break;
        }
    }

    private void ChangeMaterialColor(float tilingX)
    {
        // If the prefab has a renderer and material
        if (render != null && render.material != null && render.material.HasProperty("_MainTex") == true)
        {
            Debug.Log("changing");
            // Change the tiling of the material
            render.material.mainTextureScale = new Vector2(tilingX, 1);
        }
        else
        {
            Debug.LogWarning("Prefab does not have a Renderer or Material!");
        }
    }
}
