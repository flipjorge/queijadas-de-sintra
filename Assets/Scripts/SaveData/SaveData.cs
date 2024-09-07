using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class SaveData
{
    public static void SaveGame(int matches, int turns, int score)
    {
        var currentHighScore = LoadHighScore();

        if (score >= currentHighScore) PlayerPrefs.SetInt(HighScoreKey, score);

        var lastGames = LoadLastGames();
        var lastGamesList = lastGames.Items.ToList();
        
        lastGamesList.Insert(0, new GameDataItem(){
            Turns = turns,
            Matches = matches,
            Score = score
        });

        if (lastGamesList.Count > 10)
        {
            lastGamesList.RemoveAt(lastGamesList.Count - 1);
        }

        lastGames.Items = lastGamesList.ToArray();

        var lastGamesJson = JsonUtility.ToJson(lastGames);
        PlayerPrefs.SetString(LastGamesKey, lastGamesJson);
    }

    public static int LoadHighScore() => PlayerPrefs.GetInt(HighScoreKey);

    public static GameDataList LoadLastGames()
    {
        var lastGamesValue = PlayerPrefs.GetString(LastGamesKey);
        if(string.IsNullOrEmpty(lastGamesValue))
        {
            return new GameDataList()
            {
                Items = new GameDataItem[] { }
            };
        }
            
        return JsonUtility.FromJson<GameDataList>(lastGamesValue);
    }
    
    [MenuItem("Tools/ClearData")]
    public static void ClearData()
    {
        PlayerPrefs.DeleteKey(HighScoreKey);
        PlayerPrefs.DeleteKey(LastGamesKey);
    }

    private const string HighScoreKey = "high_score_key";
    private const string LastGamesKey = "last_games";

    [Serializable]
    public class GameDataList
    {
        public GameDataItem[] Items;
    }
    
    [Serializable]
    public struct GameDataItem
    {
        public int Matches;
        public int Turns;
        public int Score;
    }
}