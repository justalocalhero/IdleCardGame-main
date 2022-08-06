public class PrimativePack : BoosterPack
{
    public PrimativePack()
    {        
        Name = "Primative";
        Cost = 1;

        BoosterBucket commonBucket = new BoosterBucket();

        commonBucket.openCount = 5;

        commonBucket.cards.Add(new Beg());
        commonBucket.cards.Add(new Borrow());
        commonBucket.cards.Add(new Steal());
        commonBucket.cards.Add(new Snack());
        commonBucket.cards.Add(new VelvetCloak());        
        commonBucket.cards.Add(new Pawn());
        commonBucket.cards.Add(new Opportunity());
        commonBucket.cards.Add(new Luck());
        commonBucket.cards.Add(new Flourish());


        BoosterBucket rareBucket = new BoosterBucket();

        rareBucket.openCount = 1;


        buckets.Add(commonBucket);
        buckets.Add(rareBucket);
    }    
}

public class Beg : Card
{
    public Beg()
    {
        Name = "Beg";
        Cost = 1;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new AddResource(ResourceType.Coin, 1))
            .Register(new AddResource(ResourceType.Food, 2));
    }
}

public class Borrow : Card
{
    public Borrow()
    {
        Name = "Borrow";
        Cost = 1;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new DrawCards(1))
            .Register(new DiscardRandomCards(1));
    }
}

public class Steal : Card
{
    public Steal()
    {
        Name = "Steal";
        Cost = 1;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new AddResource(ResourceType.Coin, 2));
    }
}

public class VelvetCloak : Card
{
    public VelvetCloak()
    {
        Name = "Velvet Cloak";
        Cost = 2;

        RegisterTag(ArchetypeTag.Hustle);        

        ScaleResourceGain effect = new ScaleResourceGain();

        effect.timings.Add(new TurnTimer(3));

        effect.WithCardTag(ArchetypeTag.Hustle).WithScaling(new Scaling() { addedValue = 1 });

        this.Register(new ApplyEffect(effect));
    }
}

public class Snack : Card
{
    public Snack()
    {
        Name = "Snack";
        Cost = 0;

        this.Register(new PayResource(ResourceType.Food, 1))
            .Register(new AddEnergy(1));
    }
}

public class Pawn : Card
{
    public Pawn()
    {
        Name = "Pawn";
        Cost = 1;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new DiscardRandomCards(1))
            .Register(new AddResource(ResourceType.Coin, 4));
    }
}

public class Flash : Card
{
    public Flash()
    {
        Name = "Flash";

        RegisterTag(ArchetypeTag.Hustle);

        ScaleResourceGain effect = new ScaleResourceGain();

        effect.timings.Add(new TurnTimer(1));

        effect.WithCardTag(ArchetypeTag.Hustle)
            .WithScaling(new Scaling().WithAddedValue(1));
        
        this.Register(new TriggeredAction(new ApplyEffect(effect), new OnDraw()));
    }
}

public class Opportunity : Card
{
    public Opportunity()
    {
        Name = "Opportunity";
        Cost = 3;

        RegisterTag(ArchetypeTag.Hustle);
        
        this.Register(new TriggeredAction(new AddEnergy(1), new OnDraw()))
            .Register(new IncreaseMaxEnergy(1));
    }
}

public class Luck : Card
{
    public Luck()
    {
        Name = "Luck";
        Cost = 2;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new TriggeredAction(new AddResource(ResourceType.Coin, 1), new OnDraw()))
            .Register(new AddResource(ResourceType.Coin, 5));
    }
}

public class Flourish : Card
{
    public Flourish()
    {
        Name = "Flourish";
        Cost = 3;

        RegisterTag(ArchetypeTag.Hustle);

        this.Register(new DrawCards(2));
    }

    public override void CustomEffectFirst(PlayPackage playPackage)
    {
        string text = "Discard: " + playPackage.discardPile.cards.Count + " Deck: " + playPackage.deck.Cards.Count;

        for(int i = playPackage.discardPile.cards.Count - 1; i >= 0; i--)
        {
            Card card = playPackage.discardPile.cards[i];

            bool found = false;

            foreach(TriggeredAction triggeredAction in card.triggers)
            {
                if(triggeredAction.trigger is OnDraw)
                {
                    found = true;
                    break;
                }
            }

            if(found)
            {
                playPackage.discardPile.cards.Remove(card);
                playPackage.deck.Cards.Add(card);
                playPackage.deck.Shuffle();
            }
        }
        
        text += " => Discard: " + playPackage.discardPile.cards.Count + " Deck: " + playPackage.deck.Cards.Count;
    }

    public override string CustomDescriptionFirst()
    {
        return "Return all cards with on draw effects from the discard pile to the deck.";
    }
}