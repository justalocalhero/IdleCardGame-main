using UnityEngine;

public class Advance : IAction, IKeywords
{
    public Keyword[] keywords => _keywords;
    public Keyword[] _keywords = { Keyword.Advance };

    public IAction Clone()
    {
        return new Advance();
    }

    public string GetDescription()
    {
        return Keyword.Advance.Link();
    }

    public void Play(PlayPackage playPackage)
    {
        foreach(EnergyGrowth energyGrowth in playPackage.gameBoard.properties)
        {
            energyGrowth.UpdateMeter(playPackage);
        }
    }
}

public class Expand : IAction, IKeywords
{
    public int count;

    public Keyword[] keywords => _keywords;
    public Keyword[] _keywords = { Keyword.Expand };

    public Expand(int count)
    {
        this.count = count;
    }

    public IAction Clone()
    {
        return new Expand(count);
    }

    public string GetDescription()
    {
        return Keyword.Expand.Link() + " " + count;
    }

    public void Play(PlayPackage playPackage)
    {
        playPackage.gameBoard.startingFields += count;
        playPackage.gameBus.RaiseOnFieldsChanged(playPackage);
    }
}