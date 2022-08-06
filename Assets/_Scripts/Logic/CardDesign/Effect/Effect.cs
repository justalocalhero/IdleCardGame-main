using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : IClonable<Effect>
{
    public List<Timing> timings = new List<Timing>();
    
    public Effect WithTiming(Timing timing)
    {
        timings.Add(timing);

        return this;
    }

    public Effect WithTimings(List<Timing> timingList)
    {
        foreach(Timing timing in timingList)
        {
            timings.Add(timing);
        }

        return this;
    }

    public void Register(PlayPackage playPackage)
    {
        foreach(Timing timing in timings)
        {
            timing.Register(playPackage.gameBus);
            timing.onComplete += () => Remove(playPackage);
        }

        playPackage.gameBus.onEndGame += Remove;

        playPackage.gameBoard.effects.Add(this);
        playPackage.gameBus.RaiseOnEffectAdded(playPackage, this);

        OnRegister(playPackage);
    }

    public virtual void OnRegister(PlayPackage playPackage) {}

    public void Remove(PlayPackage playPackage)
    {
        foreach(Timing timing in timings)
        {
            timing.Remove(playPackage.gameBus);
            timing.onComplete -= () => Remove(playPackage);
        }

        playPackage.gameBus.onEndGame -= Remove;

        Engine.instance.gameBoard.effects.Remove(this);
        Engine.instance.gameBus.RaiseOnEffectRemoved(playPackage, this);

        OnRemove(playPackage);
    }

    public virtual void OnRemove(PlayPackage playPackage) {}

    public abstract string GetDescription();
    public abstract Effect Clone();
}
