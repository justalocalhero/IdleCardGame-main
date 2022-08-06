
public class DrawCards : IAction, IScaling, IActionTags
{
    public Scaling Scaling { get; set; }
    public int Value { get; set; }
    public GameBoard target {get; set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Draw };

    public DrawCards(int value)
    {
        Scaling = new Scaling();
        Value = value;
    }

    public string GetDescription()
    {
        int value = Scaling.Scale(Value);

        string toReturn = "Draw " + value + " Card";

        if(value > 1) toReturn += "s";

        return toReturn;
    }

    public void Play(PlayPackage playPackage)
    {
        playPackage.hand.Draw(playPackage, Scaling.Scale(Value));
    }

    public IAction Clone()
    {
        return new DrawCards(Value);
    }
}