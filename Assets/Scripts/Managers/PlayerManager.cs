using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ScriptableObject
{
    public SkillManager SkillManager { get; private set; }
    public PlayerInfoManager PlayerInfoManager { get; private set; }
    public PlayerManager()
    {
        PlayerInfoManager = new PlayerInfoManager();
    }
    public void Init()
    {
        PlayerInfoManager = new PlayerInfoManager();
    }

}
public class PlayerData
{

}