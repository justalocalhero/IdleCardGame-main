using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Direct
---
mech
-
remove max energy (incinerate)
effect increases every turn held ()

legend
-
(Cataclysm) Destroy all perms, discard all cards, exhaust all discard, gain for each

Ramp
---
mech
-
remove one cost (purge)
advance (progress)

legend
-
remove all one cost from deck, gain for each

Expansion
---
mech
-

legend
-

fill with trash
consume all
gain for each


Combo
---
mech
-

legend
-
gain for each spell
search

*/

public class AlphaPack : BoosterPack
{
    public AlphaPack()
    {        
        Name = "Alpha";
        Cost = 10;

        BoosterBucket commonBucket = new BoosterBucket();

        commonBucket.cards.Add(new Hobby());
        commonBucket.cards.Add(new Work());
        commonBucket.cards.Add(new Deliver());
        commonBucket.cards.Add(new Labor());
        commonBucket.cards.Add(new WildGrowth());
        commonBucket.cards.Add(new Progress());
        commonBucket.cards.Add(new Bob());
        commonBucket.cards.Add(new Tent());
        commonBucket.cards.Add(new Payoff());
        commonBucket.cards.Add(new Windfall());
        commonBucket.cards.Add(new Boon());
        commonBucket.cards.Add(new Shop());


        commonBucket.openCount = 5;

        // BoosterBucket rareBucket = new BoosterBucket();

        // rareBucket.openCount = 1;


        buckets.Add(commonBucket);
        //buckets.Add(rareBucket);
    }    
}

//Direct common

public class Hobby : Card
{
    public Hobby()
    {
        Name = "Hobby";
        Cost = 0;

        this.Register(new AddResource(ResourceType.Coin, 3));
    }
}

public class Work : Card
{
    public Work()
    {
        Name = "Work";
        Cost = 1;

        this.Register(new AddResource(ResourceType.Coin, 5));
    }
}

public class Deliver : Card
{
    public Deliver()
    {
        Name = "Deliver";
        Cost = 1;

        this.Register(new DrawCards(1))
            .Register(new AddResource(ResourceType.Coin, 1));
    }
}

public class Labor : Card
{
    public Labor()
    {
        Name = "Labor";
        Cost = 2;

        this.Register(new AddResource(ResourceType.Coin, 8));
    }
}

//incinerate

//Ramp Common

public class WildGrowth : Card
{
    public WildGrowth()
    {
        Name = "Wild Growth";
        Cost = 0;

        this.Register(new Advance());
    }
}

public class Progress : Card
{
    public Progress()
    {
        Name = "Progress";
        Cost = 2;

        this.Register(new Advance())
            .Register(new DrawCards(1));
    }
}

public class Bob : Card
{
    public Bob()
    {
        Name = "Bob";
        Cost = 3;

        this.Register(new Advance())
            .Register(new AddResource(ResourceType.Coin, 5));
    }
}

public class Tent : Card
{
    public Tent()
    {
        Name = "Tent";
        Cost = 3;

        this.Register(new AddPermanent(new TriggeredPermanent(Name, new OnBeginTurn(), new Advance())));
    }
}

public class Payoff : Card
{
    public Payoff()
    {
        Name = "Payoff";
        Cost = 8;

        this.Register(new AddResource(ResourceType.Coin, 22));
    }
}

public class Windfall : Card
{
    public Windfall()
    {
        Name = "Windfall";
        Cost = 5;

        this.Register(new DrawCards(4));
    }
}

public class Boon : Card
{
    public Boon()
    {
        Name = "Boon";
        Cost = 4;

        this.Register(new DrawCards(2))
            .Register(new AddResource(ResourceType.Coin, 2));
    }
}
//Expansion Common

public class Shop : Card
{
    public Shop()
    {
        Name = "Shop";
        Cost = 1;

        this.Register(new AddPermanent(new TriggeredPermanent(Name, new OnBeginTurn(), new AddResource(ResourceType.Coin, 1))));
    }
}

public class Claim : Card
{
    public Claim()
    {
        Name = "Claim";
        Cost = 2;

        this.Register(new Expand(1));
    }
}

public class Reap : Card
{
    public Reap()
    {
        Name = "Reap";
        Cost = 2;

        this.Register(new AddResourcePerProperty(ResourceType.Coin, 2, new FieldCount()));
    }
}

//Combo common

//gain max energy for a turn or two