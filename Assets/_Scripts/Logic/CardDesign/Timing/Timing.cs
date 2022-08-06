using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timing : IDescription
{
    public delegate void OnTick();
    public OnTick onTick;

    public void RaiseOnTick()
    {
        if(onTick != null) onTick();
    }

    public delegate void OnComplete();
    public OnComplete onComplete;

    public void RaiseOnComplete()
    {
        if(onComplete != null) onComplete();
    }

    public abstract void Register(GameBus gameBus);
    public abstract void Remove(GameBus gameBus);

    public abstract string GetDescription();
}

public class TurnTimer : Timing
{
    public int turns;

    public TurnTimer(int turns)
    {
        this.turns = turns;
    }

    public override string GetDescription()
    {
        return "Every " + turns + " turns: ";
    }

    public override void Register(GameBus gameBus)
    {
        gameBus.onEndTurn += Tick;
    }

    public override void Remove(GameBus gameBus)
    {
        gameBus.onEndTurn -= Tick;      
    }

    private void Tick(PlayPackage playPackage)
    {
        turns--;

        RaiseOnTick();
        if(turns <= 0) RaiseOnComplete();
    }
}

public class BeginTurnTimer : Timing
{
    public override string GetDescription()
    {
        return "Beginning of Turn: ";
    }

    public override void Register(GameBus gameBus)
    {
        gameBus.onBeginTurn += RaiseOnComplete;
    }

    public override void Remove(GameBus gameBus)
    {
        gameBus.onBeginTurn -= RaiseOnComplete;
    }

    public void RaiseOnComplete(PlayPackage playPackage)
    {
        RaiseOnComplete();
    }
}