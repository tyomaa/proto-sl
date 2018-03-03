using Sl;

public class DataStorageBase<TData, TDataWrapper> 
    where TDataWrapper : DSValueWrapper<TData, TData>
{
    private TData _data;

    public TDataWrapper Data { get; private set; }

    public void Init(TData data, TDataWrapper wrapper)
    {
        _data = data;
        Data = wrapper;
    }
}
