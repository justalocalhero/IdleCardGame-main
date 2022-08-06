using System.Collections.Generic;
using UnityEngine;

public class ScaleResourceGain : Effect
{
    public List<ResourceType> resourceTypes = new List<ResourceType>();
    public List<ArchetypeTag> cardTags = new List<ArchetypeTag>();

    public Scaling scaling = new Scaling();

    public ScaleResourceGain WithCardTag(ArchetypeTag cardTag)
    {
        cardTags.Add(cardTag);
        
        return this;
    }

    public ScaleResourceGain WithCardTags(List<ArchetypeTag> tags)
    {
        foreach(ArchetypeTag cardTag in tags)
        {
            cardTags.Add(cardTag);
        }
        
        return this;
    }

    public ScaleResourceGain WithResourceType(ResourceType resourceType)
    {
        resourceTypes.Add(resourceType);
        
        return this;
    }

    public ScaleResourceGain WithResourceTypes(List<ResourceType> types)
    {
        foreach(ResourceType resourceType in types)
        {
            resourceTypes.Add(resourceType);
        }
        
        return this;
    }

    public ScaleResourceGain WithScaling(Scaling scaling)
    {
        this.scaling.AddScaling(scaling);

        return this;
    }

    public override void OnRegister(PlayPackage playPackage)
    {
        playPackage.gameBus.scaleAction += Scale;
    }

    public override void OnRemove(PlayPackage playPackage)
    {
        playPackage.gameBus.scaleAction -= Scale;
    }

    public void Scale(PlayPackage playPackage, Card card, IAction action)
    {
        if(!AppropriateCardTag(card))
        {
            return;
        }
        if(!AppropriateResourceType(action))
        {
            return;
        }
        if(!(action is IScaling))
        {
            return;
        }

        IScaling scalable = action as IScaling;

        scalable.Scaling.AddScaling(scaling);
    }

    private bool AppropriateResourceType(IAction action)
    {
        if(resourceTypes.Count == 0) return true;
        if(!(action is IResourceType)) return false;

        IResourceType res = action as IResourceType;

        foreach(ResourceType resourceType in resourceTypes)
        {
            if(res.ResourceType == resourceType) return true;
        }


        return false;
    }

    private bool AppropriateCardTag(Card card)
    {
        if(cardTags.Count == 0) return true;

        foreach(ArchetypeTag cardTag in cardTags)
        {
            if(card.HasTag(cardTag)) return true;
        }

        return false;
    }

    public override string GetDescription()
    {
        return "Scale Resource Gain.";
    }

    public override Effect Clone()
    {
        return new ScaleResourceGain()
            .WithScaling(scaling)
            .WithCardTags(cardTags)
            .WithResourceTypes(resourceTypes)
            .WithTimings(timings);

    }
}