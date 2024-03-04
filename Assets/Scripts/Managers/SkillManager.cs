using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public PlayerSkillDB skillData;
    public PlayerSkill PlayerSkill { get; private set; }

    private Player player;

    public void Init(UserData data)
    {
        skillData = new PlayerSkillDB(data.statusId);
        PlayerSkill = new PlayerSkill(data.playerData.skillData);
    }

    public SkillData GetData()
    {
        var data = PlayerSkill.playerSkillInfo;
        SkillData skillData = new SkillData
        {
            ids = new(data.Count),
            numbers = new(data.Count)
        };

        foreach (var skill in data)
        {
            skillData.ids.Add(skill.Key);
            skillData.numbers.Add(skill.Value);
        }

        return skillData;
    }
}

[System.Serializable]
public class SkillData
{
    public List<byte> ids;
    public List<byte> numbers;
}