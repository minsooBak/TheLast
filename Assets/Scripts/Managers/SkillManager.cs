using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public PlayerSkillDB skillData;
    public PlayerSkill PlayerSkill { get; private set; }

    private byte id = 1;
    public SkillManager() 
    {
        Init();
    }
    private void Init()
    {
        skillData = new PlayerSkillDB(id);
        PlayerSkill = new PlayerSkill();
    }
}
