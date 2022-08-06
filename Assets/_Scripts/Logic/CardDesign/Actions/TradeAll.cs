public class TradeAll : IAction, IActionTags
{
    public ResourceType paidType { get; set; }
    public int paidAmount { get; set; }
    public ResourceType gainedType { get; set; }
    public int gainedAmount { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Trade };

    public TradeAll(ResourceType paidType, int paidAmount, ResourceType gainedType, int gainedAmount)
    {
        this.paidType = paidType;
        this.paidAmount = paidAmount;
        this.gainedType = gainedType;
        this.gainedAmount = gainedAmount;
    }

    public IAction Clone()
    {
        return new TradeAll(paidType, paidAmount, gainedType, gainedAmount);
    }

    public string GetDescription()
    {
        return "Trade " + paidAmount + " " + paidType + " For " + gainedType + " " + gainedAmount + " as many times as possible.";
    }

    public void Play(PlayPackage playPackage)
    {
        int amount = CanPay(playPackage);

        if(amount <= 0) return;

        playPackage.gameBoard.PayResource(new Resource(paidType, paidAmount * amount));
        playPackage.gameBoard.AddResource(new Resource(gainedType, gainedAmount * amount));
    }

    private int CanPay(PlayPackage playPackage)
    {
        return playPackage.gameBoard.GetResource(paidType) / paidAmount;
    }
}