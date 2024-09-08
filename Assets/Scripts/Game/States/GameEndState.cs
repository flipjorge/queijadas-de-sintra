using System;

public class GameEndState : State<GameManager>
{
    private readonly VoiceAudioManager _voiceAudioManager;
    private readonly Action _onRestartHandler;

    private UIGameEndedPanel _panel;

    public GameEndState(GameManager owner, VoiceAudioManager voiceAudioManager, Action onRestartHandler) : base(owner)
    {
        _voiceAudioManager = voiceAudioManager;
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
        
        _voiceAudioManager.PlayFinish();

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