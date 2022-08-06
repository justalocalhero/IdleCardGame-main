public class PayResource : ICost, IResourceType, IScaling, IActionTags
{
    public ResourceType ResourceType { get; private set; }
    public int ResourceValue { get; private set; }
    public Scaling Scaling { get; private set; }

    public ActionTag[] _actionTags = { ActionTag.Pay };
    public ActionTag[] actionTags => _actionTags;

    public PayResource(ResourceType resourceType, int resourceValue)
    {
        this.ResourceType = resourceType;
        ResourceValue = resourceValue;
        Scaling = new Scaling();
    }

    public bool CanPay(PlayPackage playPackage, Card card)
    {
        return playPackage.gameBoard.GetResource(ResourceType) >= ResourceValue;
    }

    public ICost Clone()
    {
        return new PayResource(ResourceType, ResourceValue);
    }

    public string GetDescription()
    {
        int value = Scaling.Scale(ResourceValue);

        if(value <= 0) return "";

        return Keyword.Pay.Link() + " " + value + " " + ResourceType.ToString();;
    }

    public void Pay(PlayPackage playPackage, Card card)
    {
        int value = Scaling.Scale(ResourceValue);

        if(value <= 0) return;

        playPackage.gameBoard.PayResource(new Resource(ResourceType, value));
    }
}
