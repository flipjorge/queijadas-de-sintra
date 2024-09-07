using UnityEngine;

public class CardFaceUpState : State<Card>
{
    private readonly Transform _transform;
    
    public CardFaceUpState(Card card, Transform transform) : base(card)
    {
        _transform = transform;
    }

    public override void Enter()
    {
        _transform.rotation = Quaternion.Euler(0, 0, 0);
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