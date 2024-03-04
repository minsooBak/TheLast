using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager 
{
    public SkillManager SkillManager { get; private set; }
    public PlayerInfoManager PlayerInfoManager { get; private set; }
    public ItemManager ItemManager { get; private set; }

    public void Init()
    {
        PlayerInfoManager = new PlayerInfoManager();
        ItemManager = new ItemManager();
    }

}
public class PlayerData
{

}