using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo 
{
    [field : SerializeField] public float MaxHp { get; set; }
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float TargetingRadius { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    public void SetData(int id)
    {
        GameManager.DataBases.TryGetDataBase(out EnemyDataBase enemyDataBase);
        EnemyData Data = enemyDataBase.GetData(id);
        MaxHp = Data.HP;
        Hp = Data.HP;
        MoveSpeed = 1.0f;
        TargetingRadius = 10.0f;
        AttackRange = 1.5f;
    }
}
