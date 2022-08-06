using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resource
{
    public ResourceType resourceType;
    public int count;

    public Resource(ResourceType resourceType, int count)
    {
        this.resourceType = resourceType;
        this.count = count;
    }
}

public enum ResourceType
{
    None,
    Coin,
    Food,
    Material
}

public class AddResource : IAction, IResourceType, IScaling, IActionTags
{
    public ResourceType ResourceType { get; private set; }    
    public int ResourceValue { get; private set; }    
    public Scaling Scaling { get; private set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Gain };

    public AddResource(ResourceType resourceType, int resourceValue)
    {
        ResourceType = resourceType;
        ResourceValue = resourceValue;
        Scaling = new Scaling();
    }

    public string GetDescription()
    {
        int value = Scaling.Scale(ResourceValue);

        if(value <= 0) return "";

        return Keyword.Gain.Link() + " " + value + " " + ResourceType;
    }

    public void Play(PlayPackage playPackage)
    {
        int value = Scaling.Scale(ResourceValue);

        if(value <= 0) return;

        playPackage.gameBoard.AddResource(new Resource(ResourceType, value));
    }

    public IAction Clone()
    {
        return new AddResource(ResourceType, ResourceValue);
    }
}

public class AddResourcePerProperty : IAction, IResourceType, IActionTags, IScaleWithProperty
{ 
    public ResourceType ResourceType { get; private set; }    
    public int ResourceValue { get; private set; }    

    public ActionTag[] actionTags => _actionTags;

    public IPropertyCount propertyCount { get; private set; }

    public ActionTag[] _actionTags = { ActionTag.Gain };

    public AddResourcePerProperty(ResourceType resourceType, int resourceValue, IPropertyCount propertyCount)
    {
        this.propertyCount = propertyCount;
        ResourceType = resourceType;
        ResourceValue = resourceValue;
    }

    public string GetDescription()
    {
        return Keyword.Gain.Link() + " " + ResourceValue + " " + ResourceType + " " + propertyCount.GetDescription();
    }

    public void Play(PlayPackage playPackage)
    {
        int value = ResourceValue * propertyCount.Count(playPackage);

        if(value <= 0) return;

        playPackage.gameBoard.AddResource(new Resource(ResourceType, value));
    }

    public IAction Clone()
    {
        return new AddResourcePerProperty(ResourceType, ResourceValue, propertyCount);
    }
}

public class FieldCount : IPropertyCount
{
    public int Count(PlayPackage playPackage)
    {
        return playPackage.gameBoard.fields.Count;
    }

    public string GetDescription()
    {
        return "for each Field";
    }
}

public class PermanentCount : IPropertyCount
{
    public int Count(PlayPackage playPackage)
    {
        return playPackage.gameBoard.permanents.Count;
    }

    public string GetDescription()
    {
        return "for each Permanent";
    }
}

public class EffectCount : IPropertyCount
{
    public int Count(PlayPackage playPackage)
    {
        return playPackage.gameBoard.effects.Count;
    }

    public string GetDescription()
    {
        return "for each Effect";
    }
}

public class HandCount : IPropertyCount
{
    public int Count(PlayPackage playPackage)
    {
        return playPackage.hand.cards.Count;
    }

    public string GetDescription()
    {
        return "for each Card in Hand";
    }
}

public interface IScaling
{
    Scaling Scaling { get; }
}
