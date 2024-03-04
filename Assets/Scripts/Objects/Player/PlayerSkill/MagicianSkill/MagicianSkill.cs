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
    public void SkillSelect(byte id, GameObject target, Transform player, Transform attackPoint)
    {
        switch (id)
        {
            case 101:
                if (playerInfo.Mp >= skillDB.GetData(101)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(101)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(101)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 102:
                if (playerInfo.Mp >= skillDB.GetData(102)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(102)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(102)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 103:
                if (playerInfo.Mp >= skillDB.GetData(102)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(102)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(102)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 104:
                if (target != null)
                {
                    if (playerInfo.Mp >= skillDB.GetData(104)._cost)
                    {
                        playerInfo.Mp -= skillDB.GetData(104)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(104)._prefabPath).transform.position = target.transform.position;
                    }
                }
                else
                {
                    if (playerInfo.Mp >= skillDB.GetData(104)._cost)
                    {
                        playerInfo.Mp -= skillDB.GetData(104)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(104)._prefabPath).transform.position = player.position;
                    }
                }
                break;
            case 105:
                if (playerInfo.Mp >= skillDB.GetData(105)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(105)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(105)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 106:
                if (target != null)
                {
                    if (playerInfo.Mp >= skillDB.GetData(106)._cost)
                    {
                        playerInfo.Mp -= skillDB.GetData(106)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = target.transform.position;
                    }
                }
                else
                {
                    if (playerInfo.Mp >= skillDB.GetData(106)._cost)
                    {
                        playerInfo.Mp -= skillDB.GetData(106)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = player.position;
                    }
                }
                break;
            case 107:
                if (playerInfo.Mp >= skillDB.GetData(107)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(107)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(107)._prefabPath).transform.position = player.position;
                }
                break;
            case 108:
                if (playerInfo.Mp >= skillDB.GetData(108)._cost)
                {
                    playerInfo.Mp -= skillDB.GetData(108)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(108)._prefabPath).transform.position = player.position;
                }
                break;
        }
    }
}
