using UnityEngine;
using System.Collections.Generic;

public class BoosterBucket
{
    public int openCount;
    public List<Card> cards = new List<Card>();

    public List<Card> Open()
    {
        return GetDistinct(openCount);
    }

    private List<Card> GetDistinct(int count)
    {
        List<Card> toReturn = new List<Card>();
        List<Card> pool = new List<Card>();

        pool.AddRange(cards);

        int clampedCount = Mathf.Clamp(count, 0, pool.Count);
        int remaining = count - clampedCount;

        if(remaining == count) return toReturn;

        for(int i = 0; i < clampedCount; i++)
        {
            int index = Random.Range(0, pool.Count);

            Card randomCard = pool[index].Clone();
            pool.RemoveAt(index);

            toReturn.Add(randomCard);
        }

        if(remaining == 0) return toReturn;
        else
        {
            toReturn.AddRange(GetDistinct(remaining));

            return toReturn;
        }
    }
}