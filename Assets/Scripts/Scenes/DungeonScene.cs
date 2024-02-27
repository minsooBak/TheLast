using Enums;
using UnityEngine;

public class DungeonScene : IBaseScene
{
    private DungeonDataBase _dungeonDB;
    private EnemyDataBase _enemyDB;
    private DungeonData dungeonData;
    private EnemyData enemyData;

    public int remainEnemyCount = 0;
    // 어디서 값을 가져와야 될까?
    private int level = 1;

    // FORTEST
    public Transform[] spawnPos = new Transform[3];

    public override void Init()
    {
        GameManager.DataBases.TryGetDataBase(out _dungeonDB);
        GameManager.DataBases.TryGetDataBase(out _enemyDB);
        dungeonData = _dungeonDB.GetData(level);

        int spawnedEnemyID = dungeonData.ID;
        enemyData = _enemyDB.GetData(spawnedEnemyID);
        
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        int spawnCount = dungeonData.SpawnCount;
        Debug.Log(remainEnemyCount);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = GameManager.ResourceManager.Instantiate($"Prefabs/Enemys/{enemyData.Name}");
            //enemy.transform.position = spawnPos[i].position;
            ++remainEnemyCount;
            Debug.Log(remainEnemyCount+ "" + enemy.name);
        }
    }
}
