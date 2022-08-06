using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCatalog : MonoBehaviour
{
    public DeckManager deckManager;
    public MiniCard miniCardPrefab;
    public Transform cardContainer;

    public List<CardWrapper> cards = new List<CardWrapper>();
    public List<CardWrapper> sortedCards = new List<CardWrapper>();
    private List<MiniCard> buttons = new List<MiniCard>();

    public delegate void OnNewCardAdded(CardWrapper cardWrapper);
    public OnNewCardAdded onNewCardAdded;

    public List<CardWrapper> AddCards(List<Card> cards)
    {
        List<CardWrapper> toReturn = new List<CardWrapper>();

        foreach(Card card in cards)
        {
            toReturn.Add(Add(card, 1));
        }

        return toReturn;
    }

    public CardWrapper Add(Card toAdd, int count)
    {
        foreach(CardWrapper card in cards)
        {
            if(card.card.Name == toAdd.Name) 
            {
                card.owned += count;
                return card;
            }
        }

        CardWrapper newCard = new CardWrapper() { card = toAdd, owned = count, inDeck = 0 };

        cards.Add(newCard);

        if(onNewCardAdded != null) onNewCardAdded(newCard);

        return newCard;
    }

    public void PushToButtons(List<CardWrapper> sortedCards)
    {
        this.sortedCards = sortedCards;
        int index = -1;

        for(int i = 0; i < sortedCards.Count; i++)
        {
            index++;

            CardWrapper card = sortedCards[i];
            MiniCard button;

            if(index < buttons.Count) 
            {
                button = buttons[index];
            }
            else 
            {
                button = Instantiate(miniCardPrefab, cardContainer);
                button.onClick += HandleClick;
                buttons.Add(button);
            }
            button.Register(card, index);
        }


        for(int i = buttons.Count - 1; i > index; i--)
        {
            MiniCard button = buttons[i];

            buttons.RemoveAt(i);

            button.onClick -= HandleClick;
            Destroy(button.gameObject);
        }
    }

    public void HandleClick(int index)
    {
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {            
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                deckManager.Clear(sortedCards[index]);
            else 
                deckManager.Remove(sortedCards[index]); 
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
            {
                deckManager.AddMax(sortedCards[index]);
            }
            else 
            {
                if(deckManager.CanAdd(sortedCards[index])) deckManager.Add(sortedCards[index]); 
            }
            
        }
    }

    public void PushAll()
    {
        deckManager.Add(cards);
    }
}
