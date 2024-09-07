using TMPro;
using UnityEngine;

public class UIGameStats : UIBasePanel
{
    [SerializeField] private TextMeshProUGUI HighScoreTextfield;
    [SerializeField] private TextMeshProUGUI MatchesTextfield;
    [SerializeField] private TextMeshProUGUI TurnsTextfield;
    [SerializeField] private TextMeshProUGUI ScoreTextfield;

    private GameStateData _state;
    private int _highScore;

    public void Initialize(GameStateData state, int highScore)
    {
        _state = state;
        _highScore = highScore;

        UpdateMatches(_state.Matches);
        UpdateTurns(_state.Matches);
        UpdateScore(_state.Matches);
        UpdateHighScore(_highScore);

        _state.OnMatchesChanged += UpdateMatches;
        _state.OnTurnsChanged += UpdateTurns;
        _state.OnScoreChanged += UpdateScore;
    }

    private void OnDestroy()
    {
        _state.OnMatchesChanged -= UpdateMatches;
        _state.OnTurnsChanged -= UpdateTurns;
        _state.OnScoreChanged -= UpdateScore;
    }

    private void UpdateMatches(int value)
    {
        MatchesTextfield.text = $"Matches: {value}";
    }

    private void UpdateTurns(int value)
    {
        TurnsTextfield.text = $"Turns: {value}";
    }

    private void UpdateScore(int value)
    {
        ScoreTextfield.text = $"Score: {value}";
    }

    private void UpdateHighScore(int value)
    {
        HighScoreTextfield.text = $"Highscore: {value}";
    }
}