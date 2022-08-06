public class RemovePermanent : ITargetting<Field>, IAction
{
    public Field target { get; private set; }

    public ActionTag[] actionTags => _actionTags;
    public ActionTag[] _actionTags = { ActionTag.RemovePermanent };

    public void ApplyTarget(ITarget target)
    {
        if(CanTarget(target)) this.target = target as Field;
    }

    public bool CanTarget(ITarget target)
    {
        if(!(target is Field)) return false;

        return (target as Field).Permanent != null;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public IAction Clone()
    {
        return new RemovePermanent() { target = target };
    }

    public string GetDescription()
    {
        return "Remove a Permanent";
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public void Play(PlayPackage playPackage)
    {
        target.Permanent.Remove(playPackage);
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