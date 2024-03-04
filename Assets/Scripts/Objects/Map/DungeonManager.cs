using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private DungeonDataBase _dungeonDB;
    private EnemyDataBase _enemyDB;

    public static int SelectedLevel;
    
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

        dungeonData = _dungeonDB.GetData(SelectedLevel);

        int spawnedEnemyID = dungeonData.ID;
        enemyData = _enemyDB.GetData(spawnedEnemyID);
    }
    public void LevelUp()
    {
        int nextLevel = SelectedLevel + 1;

        int maxLevel = GameManager.PlayerManager.PlayerInfoManager.userData.stageLv;
        if (maxLevel < nextLevel)
            GameManager.PlayerManager.PlayerInfoManager.userData.stageLv = nextLevel;
    }
}
