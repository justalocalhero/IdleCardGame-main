using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Permanent : ITarget, IClonable<Permanent>, IDescription
{
    public delegate void OnRemoved();
    public OnRemoved onRemoved;

    public abstract Permanent Clone();
    public abstract string GetDescription();
    public abstract string GetName();

    public void Register(PlayPackage playPackage) 
    {
        OnSetAdditions(playPackage);
        playPackage.gameBus.onEndGame += Remove;
        playPackage.gameBoard.permanents.Add(this);
    }

    public virtual void OnSetAdditions(PlayPackage playPackage) { }

    public void Remove(PlayPackage playPackage) 
    {        
        OnRemovedAdditions(playPackage);
        playPackage.gameBus.onEndGame -= Remove;
        playPackage.gameBoard.permanents.Remove(this);

        if(onRemoved != null) onRemoved();
    }

    public virtual void OnRemovedAdditions(PlayPackage playPackage) { }
}


public abstract class Field : ITarget
{
    public delegate void OnPermanentSet(Permanent permanent);
    public OnPermanentSet onPermanentSet;

    public delegate void OnPermanentRemoved(Permanent permanent);
    public OnPermanentRemoved onPermanentRemoved;

    public delegate void OnPermanentCleared();
    public OnPermanentCleared onPermanentCleared;

    public delegate void OnChanged();
    public OnChanged onChanged;

    public Permanent _permanent;
    public Permanent Permanent 
    {
        get => _permanent; 
        set
        {
            Permanent old = _permanent;
            _permanent = value;

            if(old != null) old.onRemoved -= () => Remove(old);
            if(value != null) value.onRemoved += () => Remove(value);

            if(value == null && onPermanentCleared != null) onPermanentCleared();
            if(old != null && onPermanentRemoved != null) onPermanentRemoved(old);
            if(value != null && onPermanentSet != null) onPermanentSet(value);
            if(old != value && onChanged != null) onChanged();
        }
    }

    private void Remove(Permanent permanent)
    {
        Permanent = null;
    }
}

public class TestField : Field
{

}