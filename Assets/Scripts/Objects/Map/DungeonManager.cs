using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    // 이 값을 어떻게 할까
    public static int Level = 1;
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

        // 이값은 어디서 값을 가져와야 될까?
        _level = Level;

        dungeonData = _dungeonDB.GetData(_level);

        int spawnedEnemyID = dungeonData.ID;
        enemyData = _enemyDB.GetData(spawnedEnemyID);
    }
}
