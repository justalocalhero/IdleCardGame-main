using UnityEngine;

public class IncreaseMaxEnergy : IAction, IActionTags
{
    public int EnergyValue { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.IncreaseMaxEnergy };

    public IncreaseMaxEnergy(int energyValue)
    {
        EnergyValue = energyValue;
    }

    public string GetDescription()
    {
        if(EnergyValue <= 0) return "";

        return "Increase maximum energy by " + EnergyValue;
    }

    public void Play(PlayPackage playPackage)
    {
        if(EnergyValue <= 0) return;

        playPackage.gameBoard.maxEnergy += EnergyValue;
    }

    public IAction Clone()
    {
        return new IncreaseMaxEnergy(EnergyValue);
    }
}

public class Incinerate : ICost, IKeywords
{
    public int count { get; set; }

    public Keyword[] keywords => _keywords;
    public Keyword[] _keywords = { Keyword.Incinerate };

    public Incinerate(int count)
    {
        this.count = count;
    }

    public string GetDescription()
    {
        if(count <= 0) return "";

        return Keyword.Incinerate.Link() + " " + count;
    }
    
    public ICost Clone()
    {
        return new Incinerate(count);
    }

    public bool CanPay(PlayPackage playPackage, Card card)
    {
        return playPackage.gameBoard.maxEnergy >= count;
    }

    public void Pay(PlayPackage playPackage, Card card)
    {
        if(count <= 0) return;

        int value = playPackage.gameBoard.maxEnergy - count;        
        playPackage.gameBoard.maxEnergy = Mathf.Clamp(value, 0, value);
    }
}
