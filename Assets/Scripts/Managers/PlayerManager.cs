using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager 
{
    public SkillManager SkillManager { get; private set; }
    public PlayerInfoManager PlayerInfoManager { get; private set; }
    public ItemManager ItemManager { get; private set; }
    
    public void Init(UserData userData)
    {
        PlayerInfoManager = new PlayerInfoManager(userData);
        ItemManager = new ItemManager();
        SkillManager = new SkillManager();
        userData.playerData ??= new();
        ItemManager.Init(userData.playerData.invenData);
        SkillManager.Init(userData);
    }

    public void SettingData()
    {
        if (SkillManager == null) return;

        PlayerData data = new PlayerData
        {
            skillData = SkillManager.GetData(),
            invenData = ItemManager.GetData()
        };

        PlayerInfoManager.userData.playerData = data;
    }

}

[System.Serializable]
public class PlayerData
{
    public InvenData invenData;
    public SkillData skillData;
    public SkillSlotUI[] skillSlots;
    public SlotUI[] slots;
}