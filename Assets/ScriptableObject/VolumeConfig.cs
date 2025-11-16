using UnityEngine;

[CreateAssetMenu(fileName = "VolumeConfig", menuName = "Scriptable Objects/VolumeConfig")]
public class VolumeConfig : ScriptableObject
{
    [Range(0f, 1f)]
    public float master = 1f;

    [Range(0f, 1f)]
    public float trilhas = 1f;

    [Range(0f, 1f)]
    public float sfx = 1f;
}
