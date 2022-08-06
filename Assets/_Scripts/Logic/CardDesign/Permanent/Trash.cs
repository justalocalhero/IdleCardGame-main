public class Trash : Permanent
{
    public override Permanent Clone()
    {
        return new Trash();
    }

    public override string GetDescription()
    {
        return "Trash";
    }

    public override string GetName()
    {
        return "Trash";
    }
}