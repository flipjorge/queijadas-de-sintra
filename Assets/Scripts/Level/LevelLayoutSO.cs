using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class LevelLayoutSO : ScriptableObject
{
    public LevelLayout Layout = new LevelLayout()
    {
        Columns = 2,
        Rows = 2,
        Space = new Vector2(2,2)
    };
}