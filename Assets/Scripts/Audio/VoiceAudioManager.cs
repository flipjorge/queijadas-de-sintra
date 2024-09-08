using UnityEngine;

public class VoiceAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioCollectionSO CorrectClips;
    [SerializeField] private AudioCollectionSO WrongClips;
    [SerializeField] private AudioCollectionSO StartingClips;
    [SerializeField] private AudioCollectionSO FinishClips;

    public void PlayCorrect()
    {
        AudioSource.PlayOneShot(CorrectClips.GetFirst());
    }

    public void PlayWrong()
    {
        AudioSource.PlayOneShot(WrongClips.GetFirst());
    }

    public void PlayStarting(StartingPhase phase = StartingPhase.Ready)
    {
        var clip = StartingClips.GetAt((int)phase);
        AudioSource.PlayOneShot(clip);
    }

    public void PlayFinish()
    {
        AudioSource.PlayOneShot(FinishClips.GetFirst());
    }

    public enum StartingPhase { Ready, Set, Go }
}