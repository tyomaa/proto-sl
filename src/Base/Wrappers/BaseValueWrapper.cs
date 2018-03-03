using System.Collections.Generic;
using Sl;

public abstract class BaseValueWrapper<TData> : IDSValueWrapper
{
    protected TData data_; // DO NOT RENAME
    protected string _fieldName;
    protected IDSValueWrapper _parent;

    public BaseValueWrapper(TData data, string name, IDSValueWrapper parent)
    {
        data_ = data;

        _fieldName = name;
        _parent = parent;
    }

    public List<string> GetPath()
    {
        List<string> path;
        if (_parent != null)
        {
            path = _parent.GetPath();
        }
        else 
        {
            path = new List<string>();
        }
        path.Add(_fieldName);
        return path;
    }
}
