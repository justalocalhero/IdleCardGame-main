using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<Card> Cards = new List<Card>();

    public void Add(Card card)
    {
        card.Register(Engine.instance.GetPlayPackage());
        Cards.Add(card);
    }

    public void Shuffle()
    {        
        for(int i = 0; i < Cards.Count; i++)
        {
            int index = Random.Range(0, Cards.Count);
            Card temp = Cards[i];
            Cards[i] = Cards[index];
            Cards[index] = temp;
        }
    }

    public Card Draw()
    {
        Card card = Cards[0];
        Cards.RemoveAt(0);

        return card;
    }

    public List<Card> Search(List<Card> cards)
    {
        foreach(Card card in cards)
        {
            if(Cards.Contains(card))
                Cards.Remove(card);            
        }

        return cards;
    }
    
    public Card Search(Card card)
    {
        if(!Cards.Contains(card)) Cards.Remove(card);

        return card;
    }
}
