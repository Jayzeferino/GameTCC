
using UnityEngine;

public abstract class IToolPreview : ScriptableObject
{
    public abstract void Show(RaycastHit hit);
}

// public abstract class IToolPreview : ScriptableObject
// {
//     public string itemName;
//     public Sprite icon;
//     public abstract void Show();
// }