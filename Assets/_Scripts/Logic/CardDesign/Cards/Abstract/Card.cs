using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Card : IClonable<Card>, IDescription
{
    public string Name { get; set; }
    public int Cost { get; protected set; }
    public StringBuilder Description = new StringBuilder();

    public List<ICardComponent> components = new List<ICardComponent>();

    public List<IPrompt> prompts = new List<IPrompt>();
    public List<IAction> actions = new List<IAction>();
    public List<IActionTags> actionTags = new List<IActionTags>();
    public List<IKeywords> keywords = new List<IKeywords>();
    public List<IResourceType> resourceTypes = new List<IResourceType>();
    public List<ICost> costs = new List<ICost>();
    public List<ITargetting> targets = new List<ITargetting>();

    public List<ArchetypeTag> archetypeTags = new List<ArchetypeTag>();

    public List<TriggeredAction> triggers = new List<TriggeredAction>();

    public delegate void OnDrawn(PlayPackage playPackage, Card card);
    public OnDrawn onDrawn;

    public void RaiseOnDrawn(PlayPackage playPackage)
    {
        if(onDrawn != null) onDrawn(playPackage, this);
    }

    public delegate void OnDiscarded(PlayPackage playPackage, Card card);
    public OnDiscarded onDiscarded;

    public void RaiseOnDiscarded(PlayPackage playPackage)
    {
        if(onDiscarded != null) onDiscarded(playPackage, this);
    }

    public delegate void OnPlayed(PlayPackage playPackage, Card card);
    public OnPlayed onPlayed;

    public void RaiseOnPlayed(PlayPackage playPackage)
    {
        if(onPlayed != null) onPlayed(playPackage, this);
    }

    public delegate void OnDescription(PlayPackage playPackage, Card card, StringBuilder stringBuilder);
    public OnDescription onDescription;

    public void RaiseOnDescription(PlayPackage playPackage)
    {
        if(onDescription != null) onDescription(playPackage, this, Description);
    }

    public void Register(PlayPackage playPackage)
    {
        playPackage.gameBus.onEndGame += Remove;
    }

    public void Remove(PlayPackage playPackage)
    {
        playPackage.gameBus.onEndGame -= Remove;

        for(int i = components.Count - 1; i >= 0; i--)
        {
            Remove(components[i]);
        }
    }

    public Card Register(ICardComponent cardComponent)
    {
        components.Add(cardComponent);

        if(cardComponent is IPrompt) RegisterPrompt(cardComponent as IPrompt);
        if(cardComponent is ICost) RegisterCost(cardComponent as ICost);
        if(cardComponent is IAction) RegisterAction(cardComponent as IAction);
        if(cardComponent is ITargetting) RegisterTargetting(cardComponent as ITargetting);
        if(cardComponent is TriggeredAction) RegisterTriggeredAction(cardComponent as TriggeredAction);
        if(cardComponent is IActionTags) RegisterActionTags(cardComponent as IActionTags);
        if(cardComponent is IResourceType) RegisterResourceType(cardComponent as IResourceType);
        if(cardComponent is IKeywords) RegisterKeywords(cardComponent as IKeywords);

        if(cardComponent is IOnRegisterEffects) (cardComponent as IOnRegisterEffects).OnRegister(this);

        return this;
    }

    public Card Remove(ICardComponent cardComponent)
    {
        components.Add(cardComponent);

        if(cardComponent is IPrompt) RemovePrompt(cardComponent as IPrompt);
        if(cardComponent is ICost) RemoveCost(cardComponent as ICost);
        if(cardComponent is IAction) RemoveAction(cardComponent as IAction);
        if(cardComponent is ITargetting) RemoveTargetting(cardComponent as ITargetting);
        if(cardComponent is TriggeredAction) RemoveTriggeredAction(cardComponent as TriggeredAction);
        if(cardComponent is IActionTags) RemoveActionTags(cardComponent as IActionTags);
        if(cardComponent is IResourceType) RemoveResourceType(cardComponent as IResourceType);
        if(cardComponent is IKeywords) RemoveKeywords(cardComponent as IKeywords);

        if(cardComponent is IOnRemoveEffects) (cardComponent as IOnRemoveEffects).OnRemove(this);

        return this;
    }

    private void RegisterTriggeredAction(TriggeredAction value)
    {
        triggers.Add(value);
        onDescription += value.ScaleAndDescribe;
    }

    private void RemoveTriggeredAction(TriggeredAction value)
    {
        triggers.Remove(value);
        onDescription -= value.ScaleAndDescribe;
    }

    private void RegisterCost(ICost value)
    {
        costs.Add(value);
        onPlayed += value.ScaleAndPay;
        onDescription += value.ScaleAndDescribe;
    }

    private void RemoveCost(ICost value)
    {
        costs.Remove(value);
        onPlayed -= value.ScaleAndPay;
        onDescription -= value.ScaleAndDescribe;
    }

    private void RegisterAction(IAction value)
    {
        actions.Add(value);
        onPlayed += value.ScaleAndPlay;
        onDescription += value.ScaleAndDescribe;
    }

    private void RemoveAction(IAction value)
    {
        actions.Remove(value);
        onPlayed -= value.ScaleAndPlay;
        onDescription -= value.ScaleAndDescribe;
    }

    private void RegisterPrompt(IPrompt value)
    {
        prompts.Add(value);
        value.onPromptFilled += Prompt;
    }

    private void RemovePrompt(IPrompt value)
    {
        prompts.Remove(value);
        value.onPromptFilled -= Prompt;
    }

    private void RegisterTargetting(ITargetting value)
    {
        targets.Add(value);
    }

    private void RemoveTargetting(ITargetting value)
    {
        targets.Remove(value);
    }

    private void RegisterActionTags(IActionTags value)
    {
        actionTags.Add(value);
    }

    private void RemoveActionTags(IActionTags value)
    {
        actionTags.Remove(value);
    }

    private void RegisterResourceType(IResourceType value)
    {
        resourceTypes.Add(value);
    }

    private void RemoveResourceType(IResourceType value)
    {
        resourceTypes.Remove(value);
    }

    private void RegisterKeywords(IKeywords value)
    {
        keywords.Add(value);
    }

    private void RemoveKeywords(IKeywords value)
    {
        keywords.Remove(value);
    }

    public Card RegisterTag(ArchetypeTag value)
    {
        if(!archetypeTags.Contains(value)) archetypeTags.Add(value);

        return this;
    }

    public bool HasTag(ArchetypeTag value)
    {
        return archetypeTags.Contains(value);
    }

    private void ConfirmDefaultTarget()
    {
        if(targets.Count == 0) targets.Add(new DefaultTargetting());
    }

    public bool CanTarget(ITarget potentialTarget)
    {
        ConfirmDefaultTarget();

        foreach(ITargetting target in targets)
        {
            if(target.CanTarget(potentialTarget)) return true;
        }

        return false;
    }

    public bool HasTarget()
    {
        ConfirmDefaultTarget();

        foreach(ITargetting target in targets)
        {
            if(!target.HasTarget()) return false;
        }

        return true;
    }

    public bool CanFillPrompts()
    {
        PlayPackage playPackage = Engine.instance.GetPlayPackage();

        foreach(IPrompt prompt in prompts)
        {
            if(!prompt.CanFill(playPackage)) return false;
        }

        return true;
    }

    public bool CanPayCosts()
    {
        if(Engine.instance.gameBoard.energy < Cost) return false;
        PlayPackage playPackage = Engine.instance.GetPlayPackage();
        
        if(!CanFillPrompts()) return false;

        foreach(ICost cost in costs)
        {
            if(!cost.CanPay(playPackage, this)) return false;
        }

        return true;
    }
    
    public bool CanFindTargets()
    {
        if(Engine.instance.gameBoard.energy < Cost) return false;
        PlayPackage playPackage = Engine.instance.GetPlayPackage();

        foreach(ITargetting t in targets)
        {
            if(!t.CanTargetAny(playPackage)) return false;
        }

        return true;
    }

    public void ApplyTarget(ITarget target)
    {
        ConfirmDefaultTarget();
        
        foreach(ITargetting targetting in targets)
        {
            targetting.ApplyTarget(target);
        }
    }
    
    public bool CanPlay()
    {
        if(Engine.instance.gameBoard.energy < Cost) return false;

        if(!CanPayCosts()) return false;
        if(!HasTarget()) return false;

        return true;
    }

    public void Prompt()
    {
        PlayPackage playPackage = Engine.instance.GetPlayPackage();

        foreach(IPrompt prompt in prompts)
        {
            if(!prompt.Filled())
            {
                prompt.Prompt(playPackage);

                playPackage.gameBus.RaiseOnPrompt(playPackage, prompt);
                return;
            }
        }

        Play(playPackage);
        
        foreach(IPrompt prompt in prompts)
        {
            prompt.Clear();
        }
    }

    private void Play(PlayPackage playPackage)
    {
        playPackage.gameBoard.PayEnergy(Cost);
        playPackage.hand.cards.Remove(this);

        CustomEffectFirst(playPackage);

        RaiseOnPlayed(playPackage);

        CustomEffectLast(playPackage);

        playPackage.gameBus.RaiseOnCardPlayed(playPackage, this);
    }

    public virtual void CustomEffectFirst(PlayPackage playPackage) {}
    public virtual void CustomEffectLast(PlayPackage playPackage) {}

    public string GetDescription()
    {        
        PlayPackage playPackage = Engine.instance.GetPlayPackage();

        string overrideString = OverrideDescription();

        if(overrideString != "") return overrideString;

        Description.Clear();
        Description.AppendLine(CustomDescriptionFirst());

        RaiseOnDescription(playPackage);

        Description.AppendLine(CustomDescriptionLast());

        return Description.ToString();
    }

    public virtual string CustomDescriptionFirst() { return ""; }
    public virtual string CustomDescriptionLast() { return ""; }
    public virtual string OverrideDescription() { return ""; }

    public void ClearTargets()
    {
        foreach(ITargetting target in targets)
        {
            target.ClearTarget();
        }
    }

    public virtual Card Clone()
    {
        return System.Activator.CreateInstance(this.GetType()) as Card;
    }
}