using UnityEngine;
using UnityEngine.EventSystems;

public class PromptCard : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClick(PromptCard promptCard);
    public OnClick onClick;

    public delegate void OnSelectedChanged();
    public OnSelectedChanged onSelectedChanged;

    public Card card;
    private bool _selected = false;
    public bool selected
    {
        get 
        { 
            return _selected;
        }
        set 
        { 
            _selected = value;

            if(onSelectedChanged != null) onSelectedChanged();
        }
    }

    private CardUI cardUI;

    void Awake()
    {
        cardUI = GetComponentInChildren<CardUI>();
    }

    public void Register(Card card)
    {
        this.card = card;
        cardUI.Card = card;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClick != null) onClick(this);
    }
}
