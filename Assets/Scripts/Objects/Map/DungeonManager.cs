using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private DungeonDataBase _dungeonDB;
    private EnemyDataBase _enemyDB;
    [SerializeField] private AudioClip _bgm;
    public static int SelectedLevel;
    
    [HideInInspector] public DungeonData dungeonData;
    [HideInInspector] public EnemyData enemyData;
    [HideInInspector] public int roomCount = 2;

    private void Start()
    {
        Init();
        SoundManager.Instance.PlayBGM(_bgm, Camera.main.transform.position, 10f, 10f);
    }
    private void Init()
    {
        GameManager.DataBases.TryGetDataBase(out _dungeonDB);
        GameManager.DataBases.TryGetDataBase(out _enemyDB);

        dungeonData = _dungeonDB.GetData(SelectedLevel);

        int spawnedEnemyID = dungeonData.ID;
        enemyData = _enemyDB.GetData(spawnedEnemyID);

        
    }
}
