public class AdjustMaxEnergy : Permanent
{
    public string name { get; set; }
    public int energyAmount { get; set; }

    public AdjustMaxEnergy(string name, int energyAmount)
    {
        this.name = name;
        this.energyAmount = energyAmount;
    }

    public override Permanent Clone()
    {
        return new AdjustMaxEnergy(name, energyAmount);
    }

    public override string GetName()    
    {
        return name;
    }

    public override string GetDescription()
    {
        return "Increase maximum Energy by " + energyAmount;
    }

    public override void OnSetAdditions(PlayPackage playPackage)
    {
        playPackage.gameBoard.maxEnergy += energyAmount;
    }

    public override void OnRemovedAdditions(PlayPackage playPackage)
    {
        playPackage.gameBoard.maxEnergy -= energyAmount;
    }
}
