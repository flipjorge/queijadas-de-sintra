public class StreakBonus : IBonus
{
    private int _streakCount;
    private readonly int _bonusPoints;

    public StreakBonus(int bonusPoints)
    {
        _bonusPoints = bonusPoints;
    }

    public void IncreaseStreak()
    {
        _streakCount++;
    }

    public void Reset()
    {
        _streakCount = 0;
    }
    
    public int Calculate(int score)
    {
        return score + _streakCount * _bonusPoints;
    }
}