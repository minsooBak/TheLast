using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMainRoom : MonoBehaviour
{
    [SerializeField] private DungeonManager _manager;

    [SerializeField] private Transform[] _enemySpawnPos = new Transform[4];
    [SerializeField] private DungeonDoor _door;
    private SphereCollider _collider;
    private int _remainEnemy;
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider ohter)
    { 
        if (ohter.CompareTag("Player"))
        {
            _collider.enabled = false;
            DoorClose();
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        int spawnCount = _manager.dungeonData.SpawnCount / _manager.roomCount;
        _remainEnemy = spawnCount;
        for (int i = 0; i < spawnCount; i++)
        {
            Enemy enemy = GameManager.ResourceManager.Instantiate(
                $"Prefabs/Enemys/{_manager.enemyData.Name}", GetRandomPosition())
                .GetComponent<Enemy>();
            enemy.SetData(_manager.enemyData.ID);
            enemy.Data.TargetingRadius += 10;
            enemy.HealthSystem.OnDie += EnemyDie;     
        }
    }

    private void EnemyDie()
    {
        --_remainEnemy;
        GameManager.PlayerManager.PlayerInfoManager.AddExp(_manager.enemyData.Exp);
        if (_remainEnemy <= 0)
        {
            StartCoroutine(EndRoom());
        }
    }
    private IEnumerator EndRoom()
    {
        yield return new WaitForSeconds(2.0f);
        DoorOpen();
        enabled = false;
    }
    private Vector3 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, _enemySpawnPos.Length);
        Vector3 randomPos = _enemySpawnPos[randomIndex].position;
        float x = Random.Range(0, 3);
        float z = Random.Range(0, 3);

        return randomPos + new Vector3(x, 0, z);
    }
    private void DoorOpen()
    {
        _door.DoorOpen();
    }
    private void DoorClose()
    {
        _door.DoorClose();
    }
}
