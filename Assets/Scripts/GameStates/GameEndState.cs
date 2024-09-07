using System;

public class GameEndState : State<GameManager>
{
    private readonly Action _onRestartHandler;

    private UIGameEndedPanel _panel;

    public GameEndState(GameManager owner, Action onRestartHandler) : base(owner)
    {
        _onRestartHandler = onRestartHandler;
    }

    public override async void Enter()
    {
        SaveData.SaveGame(
            Owner.GameStateData.Turns,
            Owner.GameStateData.Matches,
            Owner.GameStateData.Score
        );
        
        _panel = Owner.UI.InstantiatePanel<UIGameEndedPanel>();
        _panel.Initialize(Owner.GameStateData, _onRestartHandler);

        await _panel.Show();
    }

    public override void Update()
    {
        //
    }

    public override async void Exit()
    {
        await _panel.Hide();
    }
}