public class ResetableClass<T> : IReset where T : IClonable<T>
{
    private T baseValue;
    public T Value;

    public ResetableClass(T t)
    {
        baseValue = t.Clone();
        Value = t;
    }

    public void Reset()
    {
        Value = baseValue.Clone();
    }
}
