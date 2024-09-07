public class GameInitializeState : State<GameManager>
{
    public GameInitializeState(GameManager owner) : base(owner)
    {
        //
    }

    public override void Enter()
    {
        Owner.DealCards();
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