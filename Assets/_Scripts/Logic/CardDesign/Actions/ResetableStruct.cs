public class ResetableStruct<T> : IReset where T : struct
{
    private T baseValue;
    public T Value;

    public ResetableStruct(T t)
    {
        baseValue = t;
        Value = t;
    }

    public void Reset()
    {
        Value = baseValue;
    }
}
