using System;
using System.Collections.Generic;

public static class ListUtils
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var rand = new Random();
        var n = list.Count;
        
        for (var i = n - 1; i > 0; i--)
        {
            var j = rand.Next(i + 1);
            
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}