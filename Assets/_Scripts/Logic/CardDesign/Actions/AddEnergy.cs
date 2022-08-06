public class AddEnergy : IAction, IActionTags
{
    public int EnergyValue { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.AddEnergy };


    public AddEnergy(int energyValue)
    {
        EnergyValue = energyValue;
    }

    public string GetDescription()
    {
        if(EnergyValue <= 0) return "";

        return "Add " + EnergyValue + " Energy.";
    }

    public void Play(PlayPackage playPackage)
    {
        if(EnergyValue <= 0) return;

        playPackage.gameBoard.AddEnergy(EnergyValue);
    }

    public IAction Clone()
    {
        return new AddEnergy(EnergyValue);
    }
}
