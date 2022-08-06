public class AddPermanent : ITargetting<Field>, IAction, IActionTags
{
    public Permanent permanent;

    public Field target { get; private set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.AddPermanent };

    public AddPermanent(Permanent permanent)
    {
        this.permanent = permanent;
    }

    public string GetDescription()
    {
        return permanent.GetDescription();
    }

    public void Play(PlayPackage playPackage)
    {
        target.Permanent?.Remove(playPackage);

        target.Permanent = permanent.Clone();

        target.Permanent.Register(playPackage);
    }
    
    public IAction Clone()
    {
        return new AddPermanent(permanent) { target = target };
    }

    public bool CanTarget(ITarget target)
    {
        if(!(target is Field)) return false;

        return (target as Field).Permanent == null;
    }

    public void ApplyTarget(ITarget target)
    {
        if(CanTarget(target)) this.target = target as Field;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public bool CanTargetAny(PlayPackage playPackage)
    {
        foreach(Field field in playPackage.gameBoard.fields)
        {
            if(CanTarget(field)) return true;
        }

        return false;
    }
}