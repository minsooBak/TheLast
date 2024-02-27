using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class DungeonManager : MonoBehaviour
{
    private DungeonDataBase _dungeonDB;
    private EnemyDataBase _enemyDB;
    private DungeonData dungeonData;
    private EnemyData enemyData;

    public int remainEnemyCount = 0;

    [SerializeField] private Transform[] _enemySpawnPos = new Transform[4];
    [SerializeField] private Transform _playerSpawnPos;
    // 어디서 값을 가져와야 될까?
    private int level = 1;
    void Start()
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
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = GameManager.ResourceManager.Instantiate($"Prefabs/Enemys/{enemyData.Name}");
            enemy.transform.position = GetRandomPosition();
            ++remainEnemyCount;
            Debug.Log(remainEnemyCount + "" + enemy.name);
        }
    }
    private Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, _enemySpawnPos.Length);
        Vector3 randomPos = _enemySpawnPos[randomIndex].position;
        float x = Random.Range(0, 3);
        float z = Random.Range(0, 3);
        return randomPos + new Vector3(x, 0, z);
    }
}
