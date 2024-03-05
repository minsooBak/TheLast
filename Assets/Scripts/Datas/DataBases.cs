using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DataBases
{
    private List<IDataBaseable> _databases = new List<IDataBaseable>(10);

    public bool TryGetDataBase<T>(out T dataBase) where T : ScriptableObject
    {
        foreach(IDataBaseable datas in _databases)
        {
            if(datas is T)
            {
                dataBase = datas as T;
                return true;
            }
        }

        var data = ScriptableObject.CreateInstance<T>();
        Assert.IsTrue(data is IDataBaseable);
        var baseData = data as IDataBaseable;
        baseData.Init();
        _databases.Add(baseData);
        dataBase = data;
        return true;
    }

    public void AllSave()
    {
        foreach(var data in _databases)
        {
            data.Save();
        }
        _databases.Clear();
    }
}
