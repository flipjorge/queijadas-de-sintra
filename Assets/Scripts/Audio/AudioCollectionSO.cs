using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Audio Collection")]
public class AudioCollectionSO : ScriptableObject
{
    public AudioClip[] Clips;

    public AudioClip GetFirst() => Clips.Length > 0 ? Clips[0] : null;
    
    public AudioClip GetAt(int index) => Clips.Length >= index ? Clips[index] : null;

    public AudioClip GetRandomClip()
    {
        var index = Random.Range(0, Clips.Length - 1);
        return Clips[index];
    }
}