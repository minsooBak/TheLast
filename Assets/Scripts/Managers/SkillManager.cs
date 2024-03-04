using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public SkillManager() 
    {
        Init();
    }

    public PlayerSkillDB skillData;
    public PlayerSkill PlayerSkill { get; private set; }

    private Player player;

    private void Init()
    {
        skillData = new PlayerSkillDB(player.id);
        PlayerSkill = new PlayerSkill();
    }
}
