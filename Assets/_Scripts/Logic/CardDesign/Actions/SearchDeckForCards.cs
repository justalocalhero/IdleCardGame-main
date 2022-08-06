using System.Collections.Generic;

public class SearchDeckForCards : IAction, IPrompt<Card>, IActionTags
{
    public bool prompted = false;
    public event OnPromptFilled onPromptFilled;

    public int selectCount { get; private set; }

    public Deck deck { get; set; }

    public SearchQuerry searchQuerry = new SearchQuerry();

    public List<Card> population => searchQuerry.Process(deck.Cards);

    private List<Card> _targets = new List<Card>();
    public List<Card> targets { get { return _targets; } set {_targets = value; } }

    public ActionTag[] actionTags => _actionTags;

    public ActionTag[] _actionTags = { ActionTag.Search };


    public SearchDeckForCards(int count)
    {
        selectCount = count;
    }

    public SearchDeckForCards WithQuerry(SearchQuerry searchQuerry)
    {
        this.searchQuerry = searchQuerry;

        return this;
    }

    public IAction Clone()
    {
        SearchDeckForCards toReturn = new SearchDeckForCards(selectCount);

        toReturn.targets = targets;
        toReturn.deck = deck;
        toReturn.prompted = prompted;
        toReturn.searchQuerry = searchQuerry;

        return toReturn;
    }

    public string GetDescription()
    {
        string toReturn = "Search ";
        toReturn += (selectCount > 1) ?  selectCount + " Cards" : "a Card";

        return toReturn;
    }

    public void Prompt(PlayPackage playPackage)
    {
        deck = playPackage.deck;
        prompted = true;
    }

    public bool CanFill(PlayPackage playPackage)
    {
        return true;
    }

    public void Play(PlayPackage playPackage)
    {
        playPackage.hand.Draw(playPackage, playPackage.deck.Search(targets));

        Clear();
    }

    public void Fill(List<Card> card)
    {
        targets.AddRange(card);

        if(Filled() && onPromptFilled != null) onPromptFilled();
    }

    public void Clear()
    {
        prompted = false;
        targets.Clear();
        deck = null;
    }

    public bool Filled()
    {
        return prompted && ValidTargets(targets);
    }

    public bool ValidTargets(List<Card> targets)
    {
        return targets.Count <= selectCount;
    }

    public bool IsExcess(List<Card> targets)
    {
        return targets.Count > selectCount;
    }
}

public class SearchDiscardPileForCards : IAction, IPrompt<Card>, IActionTags
{
    public bool prompted = false;
    public event OnPromptFilled onPromptFilled;

    public int selectCount { get; private set; }

    public DiscardPile discardPile { get; set; }

    public SearchQuerry searchQuerry = new SearchQuerry();

    public List<Card> population => searchQuerry.Process(discardPile.cards);

    private List<Card> _targets = new List<Card>();
    public List<Card> targets { get { return _targets; } set {_targets = value; } }

    public ActionTag[] actionTags => _actionTags;

    public ActionTag[] _actionTags = { ActionTag.Recover };


    public SearchDiscardPileForCards(int count)
    {
        selectCount = count;
    }

    public SearchDiscardPileForCards WithQuerry(SearchQuerry searchQuerry)
    {
        this.searchQuerry = searchQuerry;
        
        return this;
    }

    public IAction Clone()
    {
        SearchDiscardPileForCards toReturn = new SearchDiscardPileForCards(selectCount);

        toReturn.targets = targets;
        toReturn.discardPile = discardPile;
        toReturn.prompted = prompted;
        toReturn.searchQuerry = searchQuerry;

        return toReturn;
    }

    public string GetDescription()
    {
        string toReturn = "Recover ";
        toReturn += (selectCount > 1) ?  selectCount + " Cards" : "a Card";
        toReturn += " From Discard Pile";

        return toReturn;
    }

    public void Prompt(PlayPackage playPackage)
    {
        prompted = true;

        discardPile = playPackage.discardPile;
    }

    public void Play(PlayPackage playPackage)
    {
        playPackage.hand.Draw(playPackage, playPackage.discardPile.Search(targets));

        Clear();
    }

    public void Fill(List<Card> card)
    {
        targets.AddRange(card);

        if(Filled() && onPromptFilled != null) onPromptFilled();
    }

    public void Clear()
    {
        prompted = false;
        targets.Clear();
        discardPile = null;
    }

    public bool Filled()
    {
        return prompted && ValidTargets(targets);
    }

    public bool ValidTargets(List<Card> targets)
    {
        return targets.Count <= selectCount;
    }

    public bool IsExcess(List<Card> targets)
    {
        return targets.Count > selectCount;
    }

    public bool CanFill(PlayPackage playPackage)
    {
        return true;
    }
}

public class DiscardFromHand : IAction, IPrompt<Card>, IActionTags, IOnRegisterEffects
{
    public bool prompted = false;
    public event OnPromptFilled onPromptFilled;

    public int selectCount { get; private set; }

    public Card parentCard { get; set; }
    public Hand hand { get; set; }

    public List<Card> population => GetCards(); 

    private List<Card> _targets = new List<Card>();
    public List<Card> targets { get { return _targets; } set {_targets = value; } }

    public ActionTag[] actionTags => _actionTags;

    public ActionTag[] _actionTags = { ActionTag.Discard };


    public DiscardFromHand(int count)
    {
        selectCount = count;
    }

    public IAction Clone()
    {
        DiscardFromHand toReturn = new DiscardFromHand(selectCount);

        toReturn.targets = targets;
        toReturn.hand = hand;
        toReturn.prompted = prompted;

        return toReturn;
    }

    private List<Card> GetCards()
    {
        List<Card> toReturn = new List<Card>();

        foreach(Card card in hand.cards)
        {
            if(card != parentCard) toReturn.Add(card);
        }

        return toReturn;
    }

    public string GetDescription()
    {
        string toReturn = "Discard ";
        toReturn += (selectCount > 1) ?  selectCount + " Cards" : "a Card";

        return toReturn;
    }

    public void Prompt(PlayPackage playPackage)
    {
        hand = playPackage.hand;
        prompted = true;
    }

    public bool CanFill(PlayPackage playPackage)
    {
        return playPackage.hand.cards.Count > selectCount;
    }

    public void Play(PlayPackage playPackage)
    {
        playPackage.hand.Discard(playPackage, targets);

        Clear();
    }

    public void OnRegister(Card card)
    {
        parentCard = card;
    }

    public void Fill(List<Card> card)
    {
        targets.AddRange(card);

        if(Filled() && onPromptFilled != null) onPromptFilled();
    }

    public void Clear()
    {
        prompted = false;
        targets.Clear();
        hand = null;
        parentCard = null;
    }

    public bool Filled()
    {
        return prompted && ValidTargets(targets);
    }

    public bool ValidTargets(List<Card> targets)
    {
        return targets.Count == selectCount;
    }

    public bool IsExcess(List<Card> targets)
    {
        return targets.Count > selectCount;
    }
}

public class ArchetypeQuerry : TagQuerry<ArchetypeTag>
{
    public ArchetypeQuerry(ArchetypeTag tag) : base(tag) { }

    public override bool In(Card card)
    {
        if(tag == ArchetypeTag.None) return true;

        return card.archetypeTags.Contains(tag);
    }
}

public class TriggerQuerry : TagQuerry<TriggerTag>
{
    public TriggerQuerry(TriggerTag tag) : base(tag) { }

    public override bool In(Card card)
    {
        if(tag == TriggerTag.None) return true;

        foreach(TriggeredAction trigger in card.triggers)
        {
            if(trigger.trigger.triggerTag == tag) return true;
        }

        return false;
    }
}

public class TagQuerry<T> : SearchQuerry
{
    private TagQuerry() { }

    public TagQuerry(T tag)
    {
        this.tag = tag;
    }

    public T tag;
}

public class SearchQuerry
{
    public List<SearchQuerry> orQuerries = new List<SearchQuerry>();
    public List<SearchQuerry> andQuerries = new List<SearchQuerry>();
    public List<SearchQuerry> notQuerries = new List<SearchQuerry>();

    public virtual bool In(Card card)
    {
        return true;
    }

    public SearchQuerry Or(SearchQuerry sq)
    {
        orQuerries.Add(sq);

        return this;
    }

    public SearchQuerry And(SearchQuerry sq)
    {
        andQuerries.Add(sq);
        
        return this;
    }

    public SearchQuerry Not(SearchQuerry sq)
    {
        notQuerries.Add(sq);

        return this;
    }

    public List<Card> Process(List<Card> population)
    {
        List<Card> toReturn = new List<Card>();

        foreach(Card card in population)
        {
            if(In(card)) toReturn.Add(card);
        }

        foreach(SearchQuerry sq in orQuerries)
        {
            foreach(Card card in sq.Process(population))
            {
                if(!toReturn.Contains(card)) toReturn.Add(card);
            }
        }

        foreach(SearchQuerry sq in andQuerries)
        {
            toReturn = sq.Process(toReturn);
        }

        foreach(SearchQuerry sq in notQuerries)
        {
            foreach(Card card in sq.Process(toReturn))
            {
                toReturn.Remove(card);
            }
        }        

        return toReturn;
    }
}