using System;
using System.Reflection;
using Google.Protobuf;
using Sl;

public class DSValueWrapper<T, TData> : BaseValueWrapper<TData>
{
    public DSValueWrapper(TData data, string name, IDSValueWrapper parent)
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
