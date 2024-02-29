using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : ScriptableObject
{
    protected PlayerSkillDB skillData;
    protected PlayerSkillInfo skillInfo;
    private Player player;

    public byte id;
    protected void Awake()
    {
        
    }
    protected void Start()
    {
        skillData = new PlayerSkillDB(player.id);
    }
}
