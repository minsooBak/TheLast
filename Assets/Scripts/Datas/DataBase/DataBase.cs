using System.Collections.Generic;
using UnityEngine;

interface IDataBaseable
{
    public void Init();
    public void Save();
}

public abstract class DataBase<T> : ScriptableObject, IDataBaseable
{
    protected Dictionary<int, T> _data = new Dictionary<int, T>();

    protected abstract void Load();

    public virtual void Save() { }

    public virtual T GetData(int key)
    {
        if(_data.ContainsKey(key))
            return _data[key];
        return default;
    }

    public void Init()
    {
        Load();
    }
}
