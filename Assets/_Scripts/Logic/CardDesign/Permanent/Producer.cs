using UnityEngine;

public class Producer : Permanent
{
    private string name;
    private Resource resource;
    private int turnTimer;
    private int remaining;

    public Producer(string name, Resource resource, int turnTimer)
    {
        this.name = name;
        this.turnTimer = turnTimer;
        this.remaining = turnTimer;
        this.resource = resource;
    }

    public override Permanent Clone()
    {
        return new Producer(name, resource, turnTimer);
    }

    public override string GetName()
    {
        return name;
    }

    public override string GetDescription()
    {
        string timerString = "Every " + turnTimer + " turn";
        if(turnTimer > 1) timerString += "s";
        string valueString = " " + resource.count + " " + resource.resourceType;
        if(resource.count > 1) valueString += "s";

        return (timerString + valueString);
    }

    public override void OnSetAdditions(PlayPackage playPackage)
    {
        playPackage.gameBus.onBeginTurn += Tick;
    }

    public override void OnRemovedAdditions(PlayPackage playPackage)
    {
        playPackage.gameBus.onBeginTurn -= Tick;
    }

    public void Tick(PlayPackage playPackage)
    {
        if(--remaining <= 0)
        {
            remaining = turnTimer;
            playPackage.gameBoard.AddResource(resource);
        }
    }
}

public class TriggeredPermanent : Permanent
{
    private string name;
    private ITrigger<GameBus> trigger;
    private IAction action;

    public TriggeredPermanent(string name, ITrigger<GameBus> trigger, IAction action)
    {
        this.name = name;
        this.trigger = trigger;
        this.action = action;
    }

    public override void OnSetAdditions(PlayPackage playPackage)
    {
        trigger.Register(playPackage.gameBus);
        trigger.onTriggerFire += Fire;
    }

    public override void OnRemovedAdditions(PlayPackage playPackage)
    {        
        trigger.Remove(playPackage.gameBus);
        trigger.onTriggerFire -= Fire;
    }

    public void Fire(PlayPackage playPackage, GameBus gameBus)
    {
        action.Play(playPackage);
    }

    public override Permanent Clone()
    {
        return new TriggeredPermanent(name, trigger, action);
    }

    public override string GetDescription()
    {
        return action.GetDescription();
    }

    public override string GetName()
    {
        return name;
    }
}