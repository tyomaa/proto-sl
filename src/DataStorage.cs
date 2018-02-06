using Sl;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using Google.Protobuf;

public class DataStorage
{
    private Data _data;

    public DataWrapper Data { get; private set; }

    public void Init(Data data)
    {
        _data = data;
        Data = new DataWrapper(_data, "data_", null);
    }
}

//================================

public class DataWrapper : DSValueWrapper<Data>
{
    public PlayerWrapper Player { get { return new PlayerWrapper(data_, "player_", this); } }
    public DSValueWrapper<int> Gold { get { return new DSValueWrapper<int>(data_, "gold_", this); } }
    public MapValueWrapper<string, int> { get { return new MapValueWrapper<string, int>(data_, "resources_", this); } }

    public DataWrapper(Data data, string name, IDSValueWrapper parent)
        : base(data, name, parent) { }
}

public class PlayerWrapper : DSValueWrapper<Player>
{
    public DSValueWrapper<string> Id { get { return new DSValueWrapper<string>(data_, "id_", this); } }
    public DSValueWrapper<string> Name { get { return new DSValueWrapper<string>(data_, "name_", this); } }

    public PlayerWrapper(Data data, string name, IDSValueWrapper parent)
        : base(data, name, parent) { }
}

//================================

public interface IDSValueWrapper
{
    List<string> GetPath();
}

public abstract class BaseValueWrapper : IDSValueWrapper
{
    protected Data data_; // DO NOT RENAME
    protected string _fieldName;
    protected IDSValueWrapper _parent;

    public BaseValueWrapper(Data data, string name, IDSValueWrapper parent)
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

public class MapValueWrapper<TKey, TValue> : DSValueWrapper<global::Google.Protobuf.Collections.MapField<TKey, TValue>>
{
    public MapValueWrapper(Data data, string name, IDSValueWrapper parent) 
        : base(data, name, parent) { }

    // public TValue Get(TKey key)
    // {
    // }
}

public class DSValueWrapper<T> : BaseValueWrapper
{
    public DSValueWrapper(Data data, string name, IDSValueWrapper parent)
        : base(data, name, parent) { }

    public void Set(T value)
    {
        var path = GetPath();
        //Console.WriteLine(string.Join(",", path));
        object container = this;
        for (int i = 0; i < path.Count; ++i)
        {
            if (i < path.Count - 1)
            {
                container = container
                    .GetType()
                    .GetField(path[i], BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(container);
            }
            else 
            {
                container
                    .GetType()
                    .GetField(path[i], BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(container, value);
            }
        }
    }

    public T Get()
    {
        var path = GetPath();
        //Console.WriteLine(string.Join(",", path));
        object container = this;
        for (int i = 0; i < path.Count; ++i)
        {
            if (i < path.Count - 1)
            {
                container = container
                    .GetType()
                    .GetField(path[i], BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(container);
            }
            else 
            {
                T value = (T)container
                    .GetType()
                    .GetField(path[i], BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(container);
                IDeepCloneable<T> cloneable = value as IDeepCloneable<T>;
                if (cloneable == null)
                {
                    return value;
                }
                return cloneable.Clone();
            }
        }
        throw new Exception("Something went wrong");
    }
}
