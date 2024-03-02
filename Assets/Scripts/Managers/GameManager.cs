using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _i;

    private void Awake()
    {
        if (_i == null) Init();
        else if (_i != this) Destroy(gameObject);
    }

    private void Init()
    {
        _i = this;
        ResourceManager = ScriptableObject.CreateInstance<ResourceManager>();
        UIManager = ScriptableObject.CreateInstance<UIManager>();
        ScenesManager = ScriptableObject.CreateInstance<ScenesManager>();
        DataBases = new();
        DontDestroyOnLoad(gameObject);
    }

    public static ResourceManager ResourceManager { get; private set; }
    public static UIManager UIManager { get; private set; }
    public static ScenesManager ScenesManager { get; private set; }
    public static DataBases DataBases { get; private set; }
    public static PlayerManager PlayerManager { get { return _playerManager ??= new(); } }
    public static EffectManager EffectManager { get { return _effectManager??= new(); } }

    private static PlayerManager _playerManager;
    private static EffectManager _effectManager;
}