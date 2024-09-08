using UnityEngine;

public class GameStateData
{
    public int Matches { get; private set; } = 0;
    public int Turns { get; private set; } = 0;
    public int Score { get; private set; } = 0;

    public event ScoreArgs OnMatchesChanged;
    public event ScoreArgs OnTurnsChanged;
    public event ScoreArgs OnScoreChanged;

    public void IncreaseMatches()
    {
        Matches++;

        OnMatchesChanged?.Invoke(Matches);
    }

    public void IncreaseTurns()
    {
        Turns++;

        OnTurnsChanged?.Invoke(Turns);
    }

    public void AddScore(int score)
    {
        Score += score;

        OnScoreChanged?.Invoke(Score);
    }

    public void Reset()
    {
        Matches = 0;
        Turns = 0;
        Score = 0;
    }

    public delegate void ScoreArgs(int value);
}