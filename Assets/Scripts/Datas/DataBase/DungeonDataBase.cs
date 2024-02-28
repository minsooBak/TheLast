using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDataBase : DataBase<DungeonData>
{
    protected override void Load()
    {
        var resource = Resources.Load<DungeonSO>("Data/DungeonSO");
        var datas = Instantiate(resource).DungeonDatas;
        if (datas == null || datas.Count == 0) return;

        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            _data.Add(data.Level, data);
        }
    }
}

[System.Serializable]
public class DungeonData
{
    [SerializeField] private int _level;
    [SerializeField] private int _id;

    // 일단 기본 보상으로 gold로 설정
    [SerializeField] private int _gold;
    [SerializeField] private int _exp;
    [SerializeField] private int _spawnCount;

    public int Level { get { return _level; } }
    public int ID { get { return _id; } }
    public int Gold { get { return _gold; } }
    public int SpawnCount { get { return _spawnCount; } }
}
