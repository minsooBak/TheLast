using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill
{
    public Dictionary<byte, byte> playerSkillInfo;
    public PlayerSkill(SkillData data)
    {
        if (data != null)
        {
            playerSkillInfo = new(data.ids.Count);
            for (int i = 0; i < data.ids.Count; ++i)
            {
                playerSkillInfo.Add(data.ids[i], data.numbers[i]);
            }
        }
        else
        {
            playerSkillInfo = new Dictionary<byte, byte>();
            {
                //attack
                playerSkillInfo.Add(101, 1);
                playerSkillInfo.Add(102, 1);
                playerSkillInfo.Add(103, 1);
                playerSkillInfo.Add(104, 1);
                playerSkillInfo.Add(105, 1);
                playerSkillInfo.Add(106, 1);
                playerSkillInfo.Add(107, 1);
                playerSkillInfo.Add(108, 1);
                //buff
                playerSkillInfo.Add(201, 0);
                playerSkillInfo.Add(202, 0);
                playerSkillInfo.Add(203, 0);
                playerSkillInfo.Add(204, 0);
                playerSkillInfo.Add(205, 0);
                playerSkillInfo.Add(206, 0);
                playerSkillInfo.Add(207, 0);
                playerSkillInfo.Add(208, 0);
            }
        }
    }
}
