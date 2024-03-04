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
    private PlayerSkill playerSkill;

    private void Init()
    {
        skillDB = GameManager.PlayerManager.SkillManager.skillData;
        playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        playerSkill = GameManager.PlayerManager.SkillManager.PlayerSkill;
    }
    public void SkillSelect(byte id, GameObject target, Transform player, Transform attackPoint)
    {
        switch (id)
        {
            case 101:
                Debug.Log("에너지볼트");
                if (playerInfo.Mp >= skillDB.GetData(101)._cost && playerSkill.playerSkillInfo[101] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(101)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(101)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 102:
                Debug.Log("파이어볼");
                if (playerInfo.Mp >= skillDB.GetData(102)._cost && playerSkill.playerSkillInfo[102] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(102)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(102)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 103:
                Debug.Log("아이스스피어");
                if (playerInfo.Mp >= skillDB.GetData(103)._cost && playerSkill.playerSkillInfo[103] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(103)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(103)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 104:
                Debug.Log("마력폭팔");
                if (target != null)
                {
                    if (playerInfo.Mp >= skillDB.GetData(104)._cost && playerSkill.playerSkillInfo[104] != 0)
                    {
                        playerInfo.Mp -= skillDB.GetData(104)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(104)._prefabPath).transform.position = target.transform.position;
                    }
                }
                else
                {
                    if (playerInfo.Mp >= skillDB.GetData(104)._cost && playerSkill.playerSkillInfo[104] != 0)
                    {
                        playerInfo.Mp -= skillDB.GetData(104)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(104)._prefabPath).transform.position = player.position;
                    }
                }
                break;
            case 105:
                Debug.Log("대격변");
                if (playerInfo.Mp >= skillDB.GetData(105)._cost && playerSkill.playerSkillInfo[105] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(105)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(105)._prefabPath).transform.position = attackPoint.position;
                }
                break;
            case 106:
                Debug.Log("메테오");
                if (target != null)
                {
                    if (playerInfo.Mp >= skillDB.GetData(106)._cost && playerSkill.playerSkillInfo[106] != 0)
                    {
                        playerInfo.Mp -= skillDB.GetData(106)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = target.transform.position;
                    }
                }
                else
                {
                    if (playerInfo.Mp >= skillDB.GetData(106)._cost && playerSkill.playerSkillInfo[106] != 0)
                    {
                        playerInfo.Mp -= skillDB.GetData(106)._cost;
                        GameManager.ResourceManager.Instantiate(skillDB.GetData(106)._prefabPath).transform.position = player.position;
                    }
                }
                break;
            case 107:
                Debug.Log("블리자드");
                if (playerInfo.Mp >= skillDB.GetData(107)._cost && playerSkill.playerSkillInfo[107] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(107)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(107)._prefabPath).transform.position = player.position;
                }
                break;
            case 108:
                Debug.Log("흑마법");
                if (playerInfo.Mp >= skillDB.GetData(108)._cost && playerSkill.playerSkillInfo[108] != 0)
                {
                    playerInfo.Mp -= skillDB.GetData(108)._cost;
                    GameManager.ResourceManager.Instantiate(skillDB.GetData(108)._prefabPath).transform.position = player.position;
                }
                break;
        }
    }
}
