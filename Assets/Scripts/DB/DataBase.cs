using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DataBase
{
    private static DataBase _db;
    public static DataBase DB
    {
        get
        {
            if (_db == null) _db = new DataBase();

            return _db;
        }
    }
    private EnemyData _enemy;
    public static EnemyData enemyData
    {
        get
        {
            if (DB._enemy == null)
                DB._enemy = new EnemyData();
            return DB._enemy;
        }
    }
}
