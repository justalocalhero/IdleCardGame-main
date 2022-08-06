using TMPro;
using UnityEngine;

public class CardCatalogBootstrap : MonoBehaviour
{
    public CardCatalog cardCatalog;

    void Start()
    {
        cardCatalog.Add(new TestExpand(), 4);
        cardCatalog.Add(new TestScaleWithCount(), 4);
        cardCatalog.Add(new TestContract(), 4);
        cardCatalog.Add(new TestDiscard(), 4);
        cardCatalog.Add(new Shop(), 4);
        cardCatalog.Add(new TestRandomDiscard(), 4);
        cardCatalog.Add(new SearchTest(), 4);
        cardCatalog.Add(new Forage(), 4);
        cardCatalog.Add(new GadgetTest(), 4);

        cardCatalog.PushAll();
    }
}

public class TestScaleWithCount : Card
{
    public TestScaleWithCount()
    {
        Name = "Test Scale With Count";
        Cost = 0;

        this.Register(new AddResourcePerProperty(ResourceType.Coin, 2, new FieldCount()));
    }
}

public class TestExpand : Card
{
    public TestExpand()
    {
        Name = "Test Expand";
        Cost = 0;

        this.Register(new Expand(2));
    }
}

public class TestContract : Card
{
    public TestContract()
    {
        Name = "Test Contract";
        Cost = 0;
    }

    public override void CustomEffectLast(PlayPackage playPackage)
    {
        base.CustomEffectLast(playPackage);

        playPackage.gameBoard.startingFields -= 1;
        playPackage.gameBus.RaiseOnFieldsChanged(playPackage);
    }
}

public class TestDiscard : Card
{
    public TestDiscard()
    {
        Name = "Test Discard";
        Cost = 0;

        this.Register(new DiscardFromHand(2));
    }
}

public class TestRandomDiscard : Card
{
    public TestRandomDiscard()
    {
        Name = "Test Random Discard";
        Cost = 0;

        this.Register(new DiscardRandomCards(2));

    }
}

public class SearchTest : Card
{
    public SearchTest()
    {
        Name = "Search Test";
        Cost = 0;

        this.Register(new SearchDeckForCards(2).WithQuerry(new ArchetypeQuerry(ArchetypeTag.Gadget).Or(new ArchetypeQuerry(ArchetypeTag.Agriculture))))
            .Register(new AddResource(ResourceType.Coin, 1));
    }
}

public class RecoverTest : Card
{
    public RecoverTest()
    {
        Name = "Recover Test";
        Cost = 0;

        this.Register(new SearchDiscardPileForCards(2))
            .Register(new AddResource(ResourceType.Coin, 1));
    }
}

public class GadgetTest : Card
{
    public GadgetTest()
    {
        Name = "GadgetTest";
        Cost = 0;

        this.RegisterTag(ArchetypeTag.Gadget);
    }
}

public class Forage : Card
{
    public Forage()
    {
        Name = "Forage";
        Cost = 1;

        this.RegisterTag(ArchetypeTag.Agriculture)
            .Register(new AddResource(ResourceType.Food, 2))
            .Register(new AddResource(ResourceType.Material, 1));
    }
}

public class Craft : Card
{
    public Craft()
    {
        Name = "Craft";
        Cost = 1;

        this.Register(new PayResource(ResourceType.Food, 1))
            .Register(new AddResource(ResourceType.Material, 3));
    }
}

public class Sell : Card
{
    public Sell()
    {
        Name = "Sell";
        Cost = 0;

        this.Register(new TradeAll(ResourceType.Material, 1, ResourceType.Coin, 1));
    }
}

public class CampSite : Card
{
    public CampSite()
    {
        Name = "Camp Site";
        Cost = 1;

        this.Register(new AddPermanent(new AdjustMaxEnergy("Camp Site", 2)));
    }
}

public class Farm : Card
{
    public Farm()
    {
        Name = "Farm";
        Cost = 1;

        this.Register(new AddPermanent(new Producer("Farm", new Resource(ResourceType.Coin, 2), 3)));
    }
}

public class Salvage : Card
{
    public Salvage()
    {
        Name = "Salvage";
        Cost = 2;

        this.Register(new RemovePermanent())
            .Register(new AddResource(ResourceType.Coin, 12));
    }
}

public class RecklessGreed : Card
{
    public RecklessGreed()
    {
        Name = "Reckless Greed";
        Cost = 1;

        this.Register(new AddPermanent(new Trash()))
            .Register(new AddResource(ResourceType.Coin, 6));
    }
}

public class CompoundInterest : Card
{
    private const int perValue = 5;

    public CompoundInterest()
    {
        Name ="Compound Interest";
        Cost = 2;

        this.Register(new AddResource(ResourceType.Coin, 1));
    }

    public override string CustomDescriptionLast()
    {
        return " Gain 1 coin for every " + perValue + " coins you have";
    }

    public override void CustomEffectLast(PlayPackage playPackage)
    {
        int coins = playPackage.gameBoard.GetResource(ResourceType.Coin);
        int value = coins / perValue;

        playPackage.gameBoard.AddResource(new Resource(ResourceType.Coin, value));
    }
}