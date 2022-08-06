using UnityEngine;

public class Scaling : IReset
{
    public int baseValue;
    public int addedValue;
    public int additiveMultiplier;
    public int multiplicativeMultiplier;

    public Scaling()
    {
        Reset();
    }

    public Scaling WithBaseValue(int value)
    {
        baseValue = value;

        return this;
    }

    public Scaling WithAddedValue(int value)
    {
        addedValue = value;

        return this;
    }

    public Scaling WithAdditiveMultiplier(int value)
    {
        additiveMultiplier = value;

        return this;
    }

    public Scaling WithMultiplicativeMultiplier(int value)
    {
        multiplicativeMultiplier = value;

        return this;
    }

    public void Reset()
    {
        baseValue = 0;
        addedValue = 0;
        additiveMultiplier = 0;
        multiplicativeMultiplier = 1;
    }

    public int Scale(int value)
    {
        return (value + baseValue) * (1 + additiveMultiplier) * multiplicativeMultiplier + addedValue;
    }

    public void AddScaling(Scaling scaling)
    {
        baseValue += scaling.baseValue;
        addedValue += scaling.addedValue;
        additiveMultiplier += scaling.additiveMultiplier;
        multiplicativeMultiplier *= scaling.multiplicativeMultiplier;
    }

    public void RemoveScaling(Scaling scaling)
    {
        baseValue -= scaling.baseValue;
        addedValue -= scaling.addedValue;
        additiveMultiplier -= scaling.additiveMultiplier;
        multiplicativeMultiplier /= scaling.multiplicativeMultiplier;

    }
}
