using Sl;

public class MapValueWrapper<TKey, TValue, TData> 
    : DSValueWrapper<global::Google.Protobuf.Collections.MapField<TKey, TValue>, TData>
{
    public MapValueWrapper(TData data, string name, IDSValueWrapper parent) 
        : base(data, name, parent) { }

    // public TValue Get(TKey key)
    // {
    // }
}