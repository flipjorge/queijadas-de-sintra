using DG.Tweening;
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
        var delay = Random.Range(0, 0.2f);
        
        _transform.DORotate(new Vector3(0, 0, 180), .15f).SetEase(Ease.OutSine).SetDelay(delay);
        _transform.DOPunchPosition(new Vector3(0, .8f, 0), .3f, 10, 0).SetDelay(delay);
        
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