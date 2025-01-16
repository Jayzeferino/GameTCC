using UnityEngine;

[CreateAssetMenu(menuName = "Itens/Interaction Preview")]
public class PreviewInterationItem : ScriptableObject
{
    [Header("Preview Informations")]
    public Material previewMaterial;
    public Material intantiateMaterial;
    public GameObject modelPrefab;
    public string itemName;
}