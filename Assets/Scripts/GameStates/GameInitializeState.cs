using System;
using System.Threading;
using System.Threading.Tasks;

public class GameInitializeState : State<GameManager>, IDisposable
{
    private readonly Action _onCompleteHandler;
    
    private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

    public GameInitializeState(GameManager owner, Action onCompleteHandler) : base(owner)
    {
        _onCompleteHandler = onCompleteHandler;
    }
    
    public override async void Enter()
    {
        Owner.GameStateData.Reset();
        
        var highScore = SaveData.LoadHighScore();

        await Task.Delay(500);
        if (_cancellationToken.IsCancellationRequested) return;
        
        var statsPanel = Owner.UI.InstantiateGUI<UIGameStats>();
        statsPanel.Initialize(Owner.GameStateData, highScore);
        
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

    public void Dispose()
    {
        _cancellationToken.Cancel();
    }
}