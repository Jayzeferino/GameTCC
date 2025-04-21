
using UnityEngine;

public abstract class IToolPreview : ScriptableObject
{
    public abstract void Show(RaycastHit hit);
}
