using System.Collections.Generic;

public class BonusManager
{
    private readonly IList<IBonus> _bonus = new List<IBonus>();

    public void AddBonus(IBonus bonus)
    {
        _bonus.Add(bonus);
    }

    public int Calculate(int baseScore)
    {
        var score = baseScore;
        
        foreach (var bonus in _bonus)
        {
            score = bonus.Calculate(score);
        }
        
        return score;
    }
}