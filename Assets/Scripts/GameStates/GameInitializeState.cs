using System;
using System.Threading.Tasks;

public class GameInitializeState : State<GameManager>
{
    private readonly Action _onCompleteHandler;

    public GameInitializeState(GameManager owner, Action onCompleteHandler) : base(owner)
    {
        _onCompleteHandler = onCompleteHandler;
    }

    public override async void Enter()
    {
        Owner.GameStateData.Reset();

        var highScore = SaveData.LoadHighScore();

        var statsPanel = Owner.UI.InstantiateGUI<UIGameStats>();
        statsPanel.Initialize(Owner.GameStateData, highScore);

        await Task.Delay(600);
        
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