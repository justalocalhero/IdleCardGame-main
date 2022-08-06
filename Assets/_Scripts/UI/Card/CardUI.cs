using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    private Card _card;
    public Card Card 
    { 
        get => _card;
        set
        {
            _card = value;
            if(onCardAdded != null) onCardAdded(value);
            UpdateUI();
        }
    }

    public delegate void OnCardAdded(Card card);
    public OnCardAdded onCardAdded;

    public delegate void OnUpdateUI(Card card);
    public OnUpdateUI onUpdateUI;

    public void UpdateUI()
    {
        if(onUpdateUI != null) onUpdateUI(Card);
    }
}
