using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDB : MonoBehaviour
{
    private Dictionary<int, EnemyData> _data = new Dictionary<int, EnemyData>();

    public EnemyDB()
    {
        var resource = Resources.Load<EnemySO>("DB/EnemeySO");
        var datas = Instantiate(resource).EnemyData;

        if (datas == null || datas.Count == 0) return;

        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            _data.Add(data.ID, data);
        }
    }

    public EnemyData Get(int id)
    {
        if(_data.ContainsKey(id))
            return _data[id];

        return null;
    }
}
