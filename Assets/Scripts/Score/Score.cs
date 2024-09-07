using UnityEngine;

public class Score
{
    public int Matches { get; private set; } = 0;
    public int Turns { get; private set; } = 0;

    public event MatchesArgs OnMatchesChanged;
    public event MatchesArgs OnTurnsChanged;

    public void IncreaseMatches()
    {
        Matches++;

        Debug.Log($"Matches:{Matches}");
        
        OnMatchesChanged?.Invoke(Matches);
    }

    public void IncreaseTurns()
    {
        Turns++;

        Debug.Log($"Turns:{Turns}");
        
        OnTurnsChanged?.Invoke(Turns);
    }

    public void Reset()
    {
        Matches = 0;
        Turns = 0;
    }

    public delegate void MatchesArgs(int matches);

    public delegate void TurnsArgs(int turns);
}