using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredAction : ICardComponent, IOnRegisterEffects, IOnRemoveEffects
{
    public IAction action { get; private set; }
    public ITrigger<Card> trigger { get; private set; }
    
    public TriggeredAction(IAction action, ITrigger<Card> trigger)
    {
        this.action = action;
        this.trigger = trigger;
    }

    public string GetDescription()
    {
        return trigger.GetDescription() + action.GetDescription();
    }

    public void ScaleAndDescribe(PlayPackage playPackage, Card card, System.Text.StringBuilder stringBuilder)
    {        
        IAction clone = action.Clone();

        playPackage.gameBus.RaiseScaleAction(playPackage, card, clone);

        stringBuilder.AppendLine(trigger.GetDescription() + clone.GetDescription());
    }

    public void OnRegister(Card card)
    {
        trigger.onTriggerFire += action.ScaleAndPlay;
        trigger.Register(card);
    }

    public void OnRemove(Card card)
    {
        trigger.onTriggerFire -= action.ScaleAndPlay;
        trigger.Remove(card);
    }
}

public class OnDraw: ITrigger<Card>
{
    public event OnTriggerFire<Card> onTriggerFire;

    private TriggerTag _triggerTag = TriggerTag.Draw;
    public TriggerTag triggerTag => _triggerTag;

    public string GetDescription()
    {
        return "On Draw: ";
    }

    public void Register(Card card)
    {
        card.onDrawn += RaiseOnTriggerFire;
    }

    public void Remove(Card card)
    {
        card.onDrawn -= RaiseOnTriggerFire;
    }

    private void RaiseOnTriggerFire(PlayPackage playPackage, Card card)
    {
        if(onTriggerFire != null) onTriggerFire(playPackage, card);
    }
}

public class OnDiscard : ITrigger<Card>
{
    public event OnTriggerFire<Card> onTriggerFire;
    
    private TriggerTag _triggerTag = TriggerTag.Discard;
    public TriggerTag triggerTag => _triggerTag;

    public string GetDescription()
    {
        return "On Discard: ";
    }

    public void Register(Card card)
    {
        card.onDiscarded += RaiseOnTriggerFire;
    }

    public void Remove(Card card)
    {
        card.onDiscarded -= RaiseOnTriggerFire;
    }

    private void RaiseOnTriggerFire(PlayPackage playPackage, Card card)
    {
        if(onTriggerFire != null) onTriggerFire(playPackage, card);
    }
}

public class OnBeginTurn : ITrigger<GameBus>
{
    private TriggerTag _triggerTag;
    public TriggerTag triggerTag => _triggerTag;

    public event OnTriggerFire<GameBus> onTriggerFire;

    public string GetDescription()
    {
        return "On Begin Turn: ";
    }

    public void Register(GameBus gameBus)
    {
        gameBus.onBeginTurn += RaiseOnTriggerFire;
    }

    public void Remove(GameBus gameBus)
    {
        gameBus.onBeginTurn -= RaiseOnTriggerFire;
    }

    private void RaiseOnTriggerFire(PlayPackage playPackage)
    {
        if(onTriggerFire != null) onTriggerFire(playPackage, playPackage.gameBus);
    }
}