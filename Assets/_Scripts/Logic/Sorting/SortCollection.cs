using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SortCollection : MonoBehaviour
{
    public TMP_InputField inputField;
    public CardCatalog cardCatalog;
    public float sortDelay;
    private float fireTime;
    private bool active;

    void Awake()
    {
        inputField.onValueChanged.AddListener(Ready);
        cardCatalog.onNewCardAdded += (CardWrapper card) => Ready(inputField.text);
    }

    void Update()
    {
        if(!active) return;
        if(fireTime > Time.time) return;

        Sort(inputField.text);
    }

    private void Ready(string key)
    {
        if(key == "") 
        {
            Sort(key);
            return;
        }

        active = true;
        fireTime = Time.time + sortDelay;
    }

    private void Sort(string key)
    {
        active = false;
        cardCatalog.PushToButtons(SortByKey(cardCatalog.cards, key));
    }

    public List<CardWrapper> SortByKey(List<CardWrapper> cards, string key)
    {
        List<SortString> sortStrings = SplitKey(key);

        return ProcessSortStrings(cards, sortStrings).toReturn;
    }

    public List<SortString> SplitKey(string key)
    {
        List<SortString> sortStrings = new List<SortString>();

        char[] space = {' '};
        char[] trim = {'+', '-'};

        string[] divided = key.Split(space, System.StringSplitOptions.RemoveEmptyEntries);

        SortType sortType = SortType.or;

        foreach(string s in divided)
        {
            if(s[0] == '+')
            {
                sortType = SortType.and;
                if(s.Length == 1) continue;
            }

            if(s[0] == '-')
            {
                sortType = SortType.not;
                if(s.Length == 1) continue;
            }

            sortStrings.Add(new SortString() { key = s.Trim(trim), type = sortType});
            sortType = SortType.or;

        }

        return sortStrings;
    }

    public SortQuerry ProcessSortStrings(List<CardWrapper> cards, List<SortString> sortStrings)
    {       
        List<string> or = new List<string>();
        List<string> and = new List<string>();
        List<string> not = new List<string>();

        foreach(SortString s in sortStrings)
        {
            switch(s.type)
            {
                case SortType.or:
                    or.Add(s.key);
                    break;
                case SortType.and:
                    and.Add(s.key);
                    break;
                case SortType.not:
                    not.Add(s.key);
                    break;
                default: 
                    break;
            }
        }

        SortQuerry querry;

        if(or.Count == 0) querry = new SortQuerry(){cards = cards, toReturn = cards, key =""};
        else querry = new SortQuerry(){cards = cards, toReturn = new List<CardWrapper>(), key =""};

        foreach(string s in or)
        {
            querry = ProcessOrKey(querry, s);
        }
        foreach(string s in and)
        {
            querry = ProcessAndKey(querry, s);
        }
        foreach(string s in not)
        {
            querry = ProcessNotKey(querry, s);
        }

        return querry;
    }

    public SortQuerry ProcessOrKey(SortQuerry querry, string key)
    {
        SortQuerry toReturn = querry;
        SortQuerry other = ProcessQuerries(toReturn.cards, key);

        foreach(CardWrapper wrapper in other.toReturn)
        {
            if(!toReturn.toReturn.Contains(wrapper)) toReturn.toReturn.Add(wrapper);
        }

        return toReturn;
    }

    public SortQuerry ProcessAndKey(SortQuerry querry, string key)
    {
        SortQuerry toReturn = querry;

        SortQuerry other = ProcessQuerries(toReturn.toReturn, key);

        toReturn.toReturn = other.toReturn;

        return toReturn;
    }

    public SortQuerry ProcessNotKey(SortQuerry querry, string key)
    {
        SortQuerry toReturn = querry;
        SortQuerry other = ProcessQuerries(toReturn.toReturn, key);

        List<CardWrapper> toSet = new List<CardWrapper>();

        foreach(CardWrapper cw in toReturn.toReturn)
        {
            if(!other.toReturn.Contains(cw)) toSet.Add(cw);
        }

        toReturn.toReturn = toSet;

        return toReturn;
    }

    private SortQuerry ProcessQuerries(List<CardWrapper> cards, string key)
    {
        SortQuerry querry = new SortQuerry()
        {
            toReturn = new List<CardWrapper>(),
            cards = cards,
            key = key
        };

        ProcessNameQuerry(querry);
        ProcessArchetypeQuerry(querry);
        ProcessKeywordQuerry(querry);
        ProcessActionQuerry(querry);
        ProcessTriggerQuerry(querry);
        ProcessResourceQuerry(querry);

        return querry;
    }

    private void ProcessNameQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            if(card.card.Name.ToLower().Contains(querry.key.ToLower())) 
            {
                querry.toReturn.Add(card);
            }
        }
    }

    private void ProcessArchetypeQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            foreach(ArchetypeTag tag in card.card.archetypeTags)
            {
                if(tag.ToString().ToLower().Contains(querry.key.ToLower())) 
                {
                    querry.toReturn.Add(card);
                    break;
                }
            }
        }
    }

    private void ProcessActionQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            bool found = false;

            foreach(IActionTags actionTags in card.card.actionTags)
            {
                if(found) break;
                foreach(ActionTag tag in actionTags.actionTags)
                {
                    if(tag.ToString().ToLower().Contains(querry.key.ToLower())) 
                    {
                        found = true;
                        break;
                    }
                }
            }

            if(found) querry.toReturn.Add(card);
        }
    }

    private void ProcessKeywordQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            bool found = false;

            foreach(IKeywords keywords in card.card.keywords)
            {
                if(found) break;
                foreach(Keyword tag in keywords.keywords)
                {
                    if(tag.ToString().ToLower().Contains(querry.key.ToLower())) 
                    {
                        found = true;
                        break;
                    }
                }
            }

            if(found) querry.toReturn.Add(card);
        }
    }

    private void ProcessTriggerQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            foreach(TriggeredAction action in card.card.triggers)
            {
                if(action.trigger.triggerTag.ToString().ToLower().Contains(querry.key.ToLower())) 
                {
                    querry.toReturn.Add(card);
                }                
            }
        }
    }

    private void ProcessResourceQuerry(SortQuerry querry)
    {
        foreach(CardWrapper card in querry.cards)
        {
            if(querry.toReturn.Contains(card)) continue;

            foreach(IResourceType resourceType in card.card.resourceTypes)
            {
                if(resourceType.ResourceType.ToString().ToLower().Contains(querry.key.ToLower())) 
                {
                    querry.toReturn.Add(card);
                }                
            }
        }
    }

    public struct SortQuerry
    {
        public List<CardWrapper> toReturn;
        public List<CardWrapper> cards;
        public string key;
    }    

    public struct SortString
    {
        public string key;
        public SortType type;
    }

    public enum SortType
    {
        or,
        and,
        not
    }
}