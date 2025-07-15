
using UnityEngine;

[CreateAssetMenu(menuName = "ButtonAction/Button action")]
public class ButtonAction : ScriptableObject
{
    public Sprite actionIcon;
    public string actionName;
    public AudioClip actionFx;
}
