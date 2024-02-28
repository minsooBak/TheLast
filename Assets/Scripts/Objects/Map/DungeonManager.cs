using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private DungeonDataBase _dungeonDB;
    private EnemyDataBase _enemyDB;

    private int _level;
    
    [HideInInspector] public DungeonData dungeonData;
    [HideInInspector] public EnemyData enemyData;
    [HideInInspector] public int roomCount = 2;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GameManager.DataBases.TryGetDataBase(out _dungeonDB);
        GameManager.DataBases.TryGetDataBase(out _enemyDB);
        _level = PlayerPrefs.GetInt("DungeonLevel", 1);

        dungeonData = _dungeonDB.GetData(_level);

        int spawnedEnemyID = dungeonData.ID;
        enemyData = _enemyDB.GetData(spawnedEnemyID);
    }
    public void LevelUp()
    {
        ++_level;
        PlayerPrefs.SetInt("DungeonLevel", _level);
    }
}
