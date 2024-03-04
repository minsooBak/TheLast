using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDungeonSpawner : MonoBehaviour
{
    [Header("SpawnedEnemyInFo")]
    [SerializeField] private int _enemyID;
    private EnemyData _enemyData;
    private int _remainEnemy;

    private void Start()
    {
        GameManager.DataBases.TryGetDataBase(out EnemyDataBase enemyDataBase);
        _enemyData = enemyDataBase.GetData(_enemyID);
        StartCoroutine(SpawnEnemy(0));
    }
    private IEnumerator SpawnEnemy(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int spawnCount = 4;
        _remainEnemy = spawnCount;
        for (int i = 0; i < spawnCount; i++)
        {
            Enemy enemy = GameManager.ResourceManager.Instantiate(
                $"Prefabs/Enemys/{_enemyData.Name}", GetRandomPosition())
                .GetComponent<Enemy>();
            enemy.SetData(_enemyData.ID);
            enemy.HealthSystem.OnDie += EnemyDie;
        }
        yield return null;
    }

    private void EnemyDie()
    {
        --_remainEnemy;
        if(_remainEnemy == 0)
        {
            StartCoroutine(SpawnEnemy(3));
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(0, 3);
        float z = Random.Range(0, 3);

        return transform.position + new Vector3(x, 0, z);
    }   
}
