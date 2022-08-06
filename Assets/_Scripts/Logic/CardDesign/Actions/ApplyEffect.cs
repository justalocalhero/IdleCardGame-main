public class ApplyEffect : IAction, IActionTags
{
    public Effect Effect{ get; private set; }
    public GameBoard target { get; set; }    

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.Effect };

    public ApplyEffect(Effect effect)
    {
        Effect = effect;
    }

    public string GetDescription()
    {
        return Effect.GetDescription();
    }

    public void Play(PlayPackage playPackage)
    {
        Effect.Clone().Register(playPackage);
    }

    public IAction Clone()
    {
        return new ApplyEffect(Effect.Clone());
    }
}