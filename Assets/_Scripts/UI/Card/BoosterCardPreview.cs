using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCardPreview : MonoBehaviour
{
    CardUI cardUI;

    private CardWrapper cardWrapper;
    public CardWrapper CardWrapper 
    { 
        get 
        {
            return cardWrapper;
        }
        set
        {
            cardWrapper = value;
            cardUI.Card = cardWrapper.card;

            if(onCardChanged != null) onCardChanged(cardWrapper);
        }
    }

    public delegate void OnCardChanged(CardWrapper cardWrapper);
    public OnCardChanged onCardChanged;

    void Awake()
    {
        cardUI = GetComponentInChildren<CardUI>();
    }
}
