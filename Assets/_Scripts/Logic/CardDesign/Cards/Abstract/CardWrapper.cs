public class CardWrapper
{
    private Card _card;
    public Card card 
    { 
        get => _card; 
        set 
        { 
            _card = value; 
            RaiseOnChanged(); 
        }
    }

    private int _owned, _inDeck;

    public int owned 
    { 
        get => _owned;
        set
        {
            _owned = value;
            RaiseOnChanged();
        } 
    }
    public int inDeck 
    { 
        get => _inDeck;
        set
        {
            _inDeck = value;
            RaiseOnChanged();
        }
    }

    public delegate void OnChanged();
    public OnChanged onChanged;

    public void RaiseOnChanged()
    {
        if(onChanged != null) onChanged();
    }
}