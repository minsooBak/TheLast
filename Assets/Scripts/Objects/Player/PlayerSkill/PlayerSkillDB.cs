using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerSkillDB
{
    private Dictionary<int, PlayerSkillInfo> _skill = new();
    private List<PlayerSkillInfo> _skillInfo;
    public PlayerSkillDB(byte _id)
    {
        var res = Resources.Load<PlayerBaseSkill>("Data/PlayerBaseSkill");
        var skillSo = Object.Instantiate(res);

        switch (_id)
        {
            case 1:
                _skillInfo = skillSo.MagicianSkill;
                break;
            case 2:
                _skillInfo = skillSo.OrcWarriorsSkill;
                break;
        }

        if (_skillInfo == null || _skillInfo.Count <= 0)
        {
            return;
        }
        var _skillInfoCount = _skillInfo.Count;

        for (int i = 0; i < _skillInfoCount; i++)
        {
            var skill = _skillInfo[i];

            if (_skill.ContainsKey(skill._id))
            {
                _skill[i] = skill;
            }
            else
            {
                _skill.Add(skill._id, skill);
            }
        }
    }
    public PlayerSkillInfo GetData(int _id) 
    {
        if (_skill.ContainsKey(_id))
        {
            return _skill[_id];
        }
        return null;
    }
}
