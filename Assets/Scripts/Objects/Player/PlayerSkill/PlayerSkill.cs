using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill
{
    public Dictionary<byte, byte> playerSkillInfo;
    public PlayerSkill()
    {
        playerSkillInfo = new Dictionary<byte, byte>();
        {   
            //attack
            playerSkillInfo.Add(101, 1);
            playerSkillInfo.Add(102, 0);
            playerSkillInfo.Add(103, 0);
            playerSkillInfo.Add(104, 0);
            playerSkillInfo.Add(105, 0);
            playerSkillInfo.Add(106, 0);
            playerSkillInfo.Add(107, 0);
            playerSkillInfo.Add(108, 0);
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
