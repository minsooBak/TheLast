using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianSkill
{
    public MagicianSkill()
    {
        Init();
    }
    private PlayerSkillDB skillDB;
    private PlayerInfo playerInfo;
    private void Init()
    {
        skillDB = GameManager.PlayerManager.SkillManager.skillData;
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
    }
    public void EnergyVolt(Transform target)
    {
        if (playerInfo.Mp >= skillDB.GetData(101)._cost)
        {
            playerInfo.Mp -= skillDB.GetData(101)._cost;
            GameManager.ResourceManager.Instantiate(skillDB.GetData(101)._prefabPath).transform.position = target.position;
        }
    }
    public void FireBall(Transform target)
    {
        if (playerInfo.Mp >= skillDB.GetData(102)._cost)
        {
            playerInfo.Mp -= skillDB.GetData(102)._cost;
            GameManager.ResourceManager.Instantiate(skillDB.GetData(102)._prefabPath).transform.position = target.position;
        }
    }
    public void Blizzard(Transform player)
    {
        if (playerInfo.Mp >= skillDB.GetData(107)._cost)
        {
            playerInfo.Mp -= skillDB.GetData(107)._cost;
            GameManager.ResourceManager.Instantiate(skillDB.GetData(107)._prefabPath).transform.position = player.position;
        }
    }
    public void Meteors(Transform target)
    {
        if (playerInfo.Mp >= skillDB.GetData(106)._cost)
        {
            playerInfo.Mp -= skillDB.GetData(106)._cost;
            GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = target.position;
        }
    }
}
