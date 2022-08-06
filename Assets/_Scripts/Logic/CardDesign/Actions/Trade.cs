public class Trade : IAction, IActionTags
{
    public ResourceType paidType { get; set; }
    public int paidAmount { get; set; }
    
    public ResourceType gainedType { get; set; }
    public int gainedAmount { get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Trade };

    public Trade(ResourceType paidType, int paidAmount, ResourceType gainedType, int gainedAmount)
    {
        this.paidType = paidType;
        this.paidAmount = paidAmount;
        this.gainedType = gainedType;
        this.gainedAmount = gainedAmount;
    }

    public IAction Clone()
    {
        return new Trade(paidType, paidAmount, gainedType, gainedAmount);
    }

    public string GetDescription()
    {
        return "Trade " + paidAmount + " " + paidType + " For " + gainedType + " " + gainedAmount + ".";
    }

    public void Play(PlayPackage playPackage)
    {
        if(!CanPay(playPackage)) return;

        playPackage.gameBoard.PayResource(new Resource(paidType, paidAmount));
        playPackage.gameBoard.AddResource(new Resource(gainedType, gainedAmount));
    }

    private bool CanPay(PlayPackage playPackage)
    {
        return playPackage.gameBoard.GetResource(paidType) >= paidAmount;
    }
}
