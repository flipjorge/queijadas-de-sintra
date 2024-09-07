using System;

public class GameInitializeState : State<GameManager>
{
    private readonly Action _onCompleteHandler;
    
    public GameInitializeState(GameManager owner, Action onCompleteHandler) : base(owner)
    {
        _onCompleteHandler = onCompleteHandler;
    }

    public override void Enter()
    {
        Owner.Score.Reset();
        
        _onCompleteHandler?.Invoke();
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