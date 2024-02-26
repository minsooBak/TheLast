using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonData
{
    private int _level;
    private int _id;

    // 일단 기본 보상으로 gold로 설정
    private int _gold;
    private int _spawnCount;

    public int Level { get { return _level; } }
    public int ID { get { return _id; } }
    public int Gold { get { return _gold; } }
    public int SpawnCount { get { return _spawnCount; } }
}
