using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private float _hp;
    [SerializeField] private float _attack;
    [SerializeField] private float _df;
    [SerializeField] private float _mf;
    [SerializeField] private int _itemID;

    public int ID { get { return _id; } }
    public string Name { get { return _name;} }
    public float MaxHP { get { return _hp; } }
    public float HP { get { return _hp;} }
    public float Attack { get { return _attack;} }
    public float Defense { get { return _df;} }
    public float MagicDefense { get { return _mf;} }
    public int ItemID { get { return ID; } }
}
