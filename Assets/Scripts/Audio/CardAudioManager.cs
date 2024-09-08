using UnityEngine;

public class CardAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioCollectionSO FlipClips;

    public void PlayFlip()
    {
        var clip = FlipClips.GetRandomClip();
        AudioSource.PlayOneShot(clip);
    }
}