public class TradeEnergy : IAction, IActionTags
{
    public int paidAmount { get; set; }
    public ResourceType gainedType { get; set; }
    public int gainedAmount { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Trade };

    public TradeEnergy(int paidAmount, ResourceType gainedType, int gainedAmount)
    {
        this.paidAmount = paidAmount;
        this.gainedType = gainedType;
        this.gainedAmount = gainedAmount;
    }

    public IAction Clone()
    {
        return new TradeEnergy(paidAmount, gainedType, gainedAmount);
    }

    public string GetDescription()
    {
        return "Trade " + paidAmount + " Energy For " + gainedType + " " + gainedAmount + ".";
    }

    public void Play(PlayPackage playPackage)
    {
        if(!CanPay(playPackage)) return;

        playPackage.gameBoard.PayEnergy(paidAmount);
        playPackage.gameBoard.AddResource(new Resource(gainedType, gainedAmount));
    }

    private bool CanPay(PlayPackage playPackage)
    {
        return playPackage.gameBoard.energy >= paidAmount;
    }
}
