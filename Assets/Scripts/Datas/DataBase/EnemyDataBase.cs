using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : DataBase<EnemyData>
{
    protected override void Load()
    {
        var resource = Resources.Load<EnemySO>("Data/EnemySO");
        var datas = Instantiate(resource).EnemyDatas;

        if (datas == null || datas.Count == 0) return;

        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            _data.Add(data.ID, data);
        }
    }
}

[System.Serializable]
public class EnemyData
{
    [SerializeField] private int _id;
    [SerializeField] private string _path;
    [SerializeField] private float _hp;
    [SerializeField] private float _attack;
    [SerializeField] private float _df;
    [SerializeField] private float _mf;
    [SerializeField] private int _exp;
    [SerializeField] private int _itemID;

    public int ID { get { return _id; } }
    public string Name { get { return _path; } }   
    public float HP { get { return _hp; } }
    public float Attack { get { return _attack; } }
    public float Defense { get { return _df; } }
    public float MagicDefense { get { return _mf; } }
    public int Exp { get { return _exp; } }
    public int ItemID { get { return ID; } }
}

