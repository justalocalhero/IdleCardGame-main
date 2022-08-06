using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    public List<Card> cards = new List<Card>();

    public void Draw(PlayPackage playPackage, List<Card> cards)
    {
        foreach(Card card in cards)
        {
            Draw(playPackage, card);
        }
    }

    public void Draw(PlayPackage playPackage, Card card)
    {
        cards.Add(card);
        
        card.RaiseOnDrawn(playPackage);
        playPackage.gameBus.RaiseOnCardDrawn(playPackage, card);
    }

    public void Draw(PlayPackage playPackage, int count)
    {
        for(int i = 0; i < count; i++)
        {
            if(playPackage.deck.Cards.Count == 0) 
            {
                Engine.instance.EndGame();
                return;
            }
            
            Card card = playPackage.deck.Draw();

            Draw(playPackage, card);
        }
    }

    public void DiscardRandom(PlayPackage playPackage, int count)
    {       
        List<Card> toDiscard = new List<Card>();

        for(int i = 0; i < count; i++)
        {
            if(cards.Count == 0) return;

            toDiscard.Add(cards[Random.Range(0, cards.Count)]);
        }

        Discard(playPackage, toDiscard);
    }

    public void Discard(PlayPackage playPackage, List<Card> targets)
    {       
        foreach(Card card in targets)
        {
            if(cards.Contains(card)) cards.Remove(card);

            card.RaiseOnDiscarded(playPackage);
            playPackage.gameBus.RaiseOnCardDiscarded(playPackage, card);
        }
    }
}

public class DiscardPile
{
    public List<Card> cards = new List<Card>();

    public DiscardPile(GameBus gameBus)
    {
        gameBus.onCardDiscarded += Add;
        gameBus.onCardPlayed += Add;
    }

    public void Add(PlayPackage playPackage, Card card)
    {
        cards.Add(card);
    }

    public List<Card> Search(List<Card> targets)
    {
        foreach(Card card in targets)
        {
            if(cards.Contains(card))
                cards.Remove(card);            
        }

        return targets;
    }
    
    public Card Search(Card card)
    {
        if(!cards.Contains(card)) cards.Remove(card);

        return card;
    }
}