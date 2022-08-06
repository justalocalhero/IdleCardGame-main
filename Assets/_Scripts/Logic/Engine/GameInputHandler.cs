using UnityEngine;

public class GameInputHandler : MonoBehaviour
{
    public delegate void OnCardSelected(CardController card);
    public OnCardSelected onCardSelected;

    public delegate void OnCardDeselected(CardController card);
    public OnCardDeselected onCardDeselected;

    public delegate void OnCardCleared();
    public OnCardCleared onCardCleared;

    private CardController _selected;
    public CardController SelectedCard 
    { 
        get { return _selected; }
        set
        {
            CardController old = _selected;

            _selected = value;
            
            if(old != null && onCardDeselected != null) onCardDeselected(old);
            if(_selected == null && onCardCleared != null) onCardCleared();
            if(_selected != null && onCardSelected != null) onCardSelected(value);
        }
    }
}
