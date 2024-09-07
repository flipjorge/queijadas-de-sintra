using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameEndedPanel : UIBasePanel
{
    [SerializeField] public TextMeshProUGUI MatchesTextfield;
    [SerializeField] public TextMeshProUGUI TurnsTextfield;
    [SerializeField] public TextMeshProUGUI ScoreTextfield;

    [SerializeField] public Button RestartButton;

    private readonly TaskCompletionSource<bool> _holdingTask = new TaskCompletionSource<bool>();

    private Action _onRestartHandler;
    
    public void Initialize(GameStateData gameStateData, Action onRestartHandler)
    {
        _onRestartHandler = onRestartHandler;
        
        MatchesTextfield.text = $"Matches: {gameStateData.Matches}";
        TurnsTextfield.text = $"Turns: {gameStateData.Turns}";
        ScoreTextfield.text = $"Score: {gameStateData.Score}";
    }

    private void Start()
    {
        RestartButton.onClick.AddListener(OnRestartSelectedHandler);
    }

    public override async Task Hide(int duration = 0)
    {
        await _holdingTask.Task;
        
        Destroy(this.gameObject);
    }

    private void OnRestartSelectedHandler()
    {
        _onRestartHandler?.Invoke();
        _holdingTask.SetResult(true);
    }
}