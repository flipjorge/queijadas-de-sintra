using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprites Collection")]
public class SpritesCollectionSO : ScriptableObject
{
    public Sprite[] Sprites;

    public IList<Sprite> GetInRandomOrder()
    {
        var shuffledTextures = new List<Sprite>(Sprites);
        shuffledTextures.Shuffle();
        
        return shuffledTextures;
    }
}