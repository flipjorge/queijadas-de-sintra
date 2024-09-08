using UnityEngine;

public class CardFaceDownState : State<Card>
{
    private readonly Transform _transform;
    private readonly CardAudioManager _audioManager;

    public CardFaceDownState(
        Card card,
        Transform transform,
        CardAudioManager audioManager) : base(card)
    {
        _transform = transform;
        _audioManager = audioManager;
    }

    public override void Enter()
    {
        _transform.rotation = Quaternion.Euler(0, 0, 180);
        _audioManager.PlayFlip();
    }

    public override void Update()
    {
        //
    }

    public override void Exit()
    {
        //
    }
}