using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckManager : MonoBehaviour
{
    public Color invalidColor, invalidTextColor;
    private Color defaultColor, defaultTextColor;
    public Image background;
    public TextMeshProUGUI text;

    public MiniCard miniCardPrefab;
    public Transform cardContainer;

    public List<CardWrapper> cards = new List<CardWrapper>();
    private List<MiniCard> buttons = new List<MiniCard>();

    public const int maxPerName = 4, deckSize = 20;

    public delegate void OnChanged();
    public OnChanged onChanged;

    void Awake()
    {
        defaultColor = background.color;
        defaultTextColor = text.color;
    }

    void Start()
    {
        UpdateUI();
    }

    public bool CanAdd(CardWrapper toAdd)
    {
        if(toAdd.owned <= toAdd.inDeck) return false;
        if(toAdd.inDeck >= maxPerName) return false;

        return true;
    }

    public void AddMax(CardWrapper toAdd)
    {
        int dif = toAdd.owned - toAdd.inDeck;

        int clamped = Mathf.Clamp(dif, dif, maxPerName);
        
        for(int i = 0; i < clamped; i++)
        {
            Add(toAdd, false);
        }

        UpdateUI();
    }

    public void Add(CardWrapper toAdd)
    {
        Add(toAdd, true);
    }

    private void Add(CardWrapper toAdd, bool toUpdate)
    {
        toAdd.inDeck++;

        if(toAdd.inDeck > 1) 
        {
            if(toUpdate) UpdateUI();
            return;
        }
        int index = 0;

        for(int i = 0; i < cards.Count; i++)
        {            
            if(toAdd.card.Name.CompareTo(cards[i].card.Name) <= 0) 
            {
                index = i;
                break;
            }
        }

        cards.Insert(index, toAdd);        

        MiniCard newButton = Instantiate(miniCardPrefab, cardContainer);
        newButton.Register(toAdd, index);
        newButton.onClick += HandleClick;

        buttons.Insert(index, newButton);

        for(int i = index + 1; i < buttons.Count; i++)
        {
            buttons[i].index++;
        };

        if(toUpdate) UpdateUI();
    }

    public void Add(List<CardWrapper> cards)
    {
        foreach(CardWrapper card in cards)
        {
            for(int i = 0; i < card.owned; i++) Add(card, false);
        }

        UpdateUI();
    }

    public void Remove(CardWrapper toRemove)
    {
        Remove(toRemove, true);
    }

    private void Remove(CardWrapper toRemove, bool toUpdate)
    {
        toRemove.inDeck--;

        if(toRemove.inDeck > 0) 
        {
            if(toUpdate) UpdateUI();
            return;
        }

        for(int i = 0; i < cards.Count; i++)
        {
            if(toRemove.card.Name == cards[i].card.Name) 
            {
                cards.RemoveAt(i);
                MiniCard toDestroy = buttons[i];
                buttons.RemoveAt(i);
                Destroy(toDestroy.gameObject);

                for(int j = i; j < buttons.Count; j++)
                {
                    buttons[j].index--;
                }
            }
        }

        if(toUpdate) UpdateUI();
    }

    public void Clear(CardWrapper toRemove)
    {
        int dif = toRemove.inDeck;

        for(int i = 0; i < dif; i++)
        {
            Remove(toRemove, false);
        }

        UpdateUI();
    }

    public Deck GetDeck()
    {
        Deck toReturn = new Deck();

        foreach(CardWrapper card in cards)
        {
            for(int i = 0; i < card.inDeck; i++)
            {
                toReturn.Add(card.card.Clone());
            }
        }

        return toReturn;
    }

    public int CardCount()
    {
        int toReturn = 0;

        foreach(CardWrapper card in cards)
        {
            toReturn += card.inDeck;
        }

        return toReturn;
    }

    public void UpdateUI()
    {
        if(ValidDeck()) 
        {
            text.color = defaultTextColor;
            background.color = defaultColor;
        }
        else 
        {
            text.color = invalidTextColor;
            background.color = invalidColor;
        }

        text.SetText("Deck\n" + CardCount() + " / " + deckSize);
        
        if(onChanged != null) onChanged();
    }

    public bool ValidDeck()
    {
        //return CardCount() == deckSize;
        return CardCount() > 5;
    }

    public void HandleClick(int index)
    {
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
            {
                AddMax(cards[index]);
            }
            else 
            {
                if(CanAdd(cards[index])) Add(cards[index]); 
            }
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                Clear(cards[index]);
            else 
                Remove(cards[index]); 
        }            
    }
}
