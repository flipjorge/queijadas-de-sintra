using UnityEngine;

public class CardFaceDownState : State<Card>
{
    private readonly Transform _transform;
    
    public CardFaceDownState(Card card, Transform transform) : base(card)
    {
        _transform = transform;
    }

    public override void Enter()
    {
        _transform.rotation = Quaternion.Euler(0, 0, 180);
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