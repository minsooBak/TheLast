using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
public class PlayerInfoManager
{
    public PlayerInfoManager(UserData data)
    {
        userData = data;
        Init();
    }
    private PlayerStatusDB statusDB;
    private PlayerLevelDB levelDB;

    private PlayerStatusInfo statusInfo;
    private PlayerLevelInfo levelInfo;
    public UserData userData;// CharacterSelectScene에서 넘긴 데이터
    private int statusPoint = 3;
    private int skillsPoint = 5;

    public PlayerInfo PlayerInfo { get; private set; }
    private void Init()
    {
        PlayerInfo = new PlayerInfo();
        statusDB = new PlayerStatusDB();
        levelDB = new PlayerLevelDB(userData.statusId);
        CreateCharacterStatus(userData.statusId);
    }
    private void CreateCharacterStatus(byte _id)
    {
        statusInfo = statusDB.GetData(_id);
        PlayerInfo.PlayerName = userData.characterName;
        PlayerInfo.Job += statusInfo._job;
        PlayerInfo.Hp += statusInfo._hp;
        PlayerInfo.MaxHp += statusInfo._hp;
        PlayerInfo.Mp += statusInfo._mp;
        PlayerInfo.MaxMp += statusInfo._mp;
        PlayerInfo.ADef += statusInfo._adef;
        PlayerInfo.MDef += statusInfo._mdef;
        PlayerInfo.StatStr += statusInfo._str;
        PlayerInfo.StatInt += statusInfo._int;
        PlayerInfo.StatLuk += statusInfo._luk;
        PlayerInfo.Jump += statusInfo._jump;
        PlayerInfo.Speed += statusInfo._speed;
    }
    public void LoadLevel(float exp)
    {
        PlayerInfo.Exp += exp;
        for (int i = 1; i < levelDB.GetLevelCount() + 1; i++)
        {
            if (levelDB.GetData(i)._exp <= PlayerInfo.Exp
                && PlayerInfo.Exp < levelDB.GetData(i + 1)._exp)
            {
                PlayerInfo.Level = (short)i;
                levelInfo = levelDB.GetData(PlayerInfo.Level);


                PlayerInfo.Hp += levelInfo._maxHp;
                PlayerInfo.MaxHp += levelInfo._maxHp;
                PlayerInfo.Mp += levelInfo._maxMp;
                PlayerInfo.MaxMp += levelInfo._maxMp;
                PlayerInfo.ADef += levelInfo._adef;
                PlayerInfo.MDef += levelInfo._mdef;
                PlayerInfo.StatStr += levelInfo._str;
                PlayerInfo.StatInt += levelInfo._int;
                PlayerInfo.StatLuk += levelInfo._luk;

                //PlayerInfo.StrUpPoint = PlayerInfo.strUpPoint;
                //PlayerInfo.IntUpPoint = PlayerInfo.intUpPoint;
                //PlayerInfo.LukUpPoint = PlayerInfo.lukUpPoint;

                int Point = (i * statusPoint) - (PlayerInfo.StrUpPoint
                    + PlayerInfo.IntUpPoint
                    + PlayerInfo.LukUpPoint);

                PlayerInfo.StatPoint = Point;
            }
        }
    }

    public float GetMaxExp()
    {
        return levelDB.GetData(PlayerInfo.Level + 1)._exp;
    }

    public void AddExp(float exp)
    {
        PlayerInfo.Exp += exp;

        for (int i = 1; i < levelDB.GetLevelCount() + 1; i++)
        {
            if (levelDB.GetData(i)._exp <= PlayerInfo.Exp
                && PlayerInfo.Exp < levelDB.GetData(i + 1)._exp)
            {
                if (PlayerInfo.Level != (short)i)
                {
                    PlayerInfo.Level = (short)i;
                    PlayerInfo.SkillPoint += skillsPoint;
                    PlayerInfo.StatPoint += statusPoint;
                    LevelUp();
                }
            }
        }

        userData.exp = PlayerInfo.Exp;
        userData.Level = PlayerInfo.Level;
    }
    private void LevelUp()
    {
        levelInfo = levelDB.GetData(PlayerInfo.Level - 1);
        if (levelInfo == null)
        {
            Debug.Log("존재하지 않는 레벨입니다");
            return;
        }
            PlayerInfo.MaxHp -= levelInfo._maxHp;
        PlayerInfo.MaxMp -= levelInfo._maxMp;
        PlayerInfo.ADef -= levelInfo._adef;
        PlayerInfo.MDef -= levelInfo._mdef;
        PlayerInfo.StatStr -= levelInfo._str;
        PlayerInfo.StatInt -= levelInfo._int;
        PlayerInfo.StatLuk -= levelInfo._luk;

        levelInfo = levelDB.GetData(PlayerInfo.Level);

        PlayerInfo.MaxHp += levelInfo._maxHp;
        PlayerInfo.MaxMp += levelInfo._maxMp;
        PlayerInfo.ADef += levelInfo._adef;
        PlayerInfo.MDef += levelInfo._mdef;
        PlayerInfo.StatStr += levelInfo._str;
        PlayerInfo.StatInt += levelInfo._int;
        PlayerInfo.StatLuk += levelInfo._luk;
    }
    public void StrUp()
    {
        if (PlayerInfo.StatPoint != 0)
        {
            PlayerInfo.StatPoint -= 1;
            PlayerInfo.StatStr += 1;
            PlayerInfo.StrUpPoint += 1;
        }
    }
    public void IntUp()
    {
        if (PlayerInfo.StatPoint != 0)
        {
            PlayerInfo.StatPoint -= 1;
            PlayerInfo.StatInt += 1;
            PlayerInfo.IntUpPoint += 1;
        }
    }
    public void LukUp()
    {
        if (PlayerInfo.StatPoint != 0)
        {
            PlayerInfo.StatPoint -= 1;
            PlayerInfo.StatLuk += 1;
            PlayerInfo.LukUpPoint += 1;
        }
    }
    public void EquipItem(ItemEntity item)
    {
        PlayerInfo.MaxHp += item.HP;
        PlayerInfo.MaxMp += item.MP;
        PlayerInfo.ADef += item.ADEF;
        PlayerInfo.MDef += item.MDEF;
        PlayerInfo.StatStr += item.STR;
        PlayerInfo.StatInt += item.INT;
        PlayerInfo.StatLuk += item.LUK;
        PlayerInfo.Speed += item.Speed;
        PlayerInfo.Jump += item.Jump;
    }
    public void UnEquipItem(ItemEntity item)
    {
        PlayerInfo.MaxHp -= item.HP;
        PlayerInfo.MaxMp -= item.MP;
        PlayerInfo.ADef -= item.ADEF;
        PlayerInfo.MDef -= item.MDEF;
        PlayerInfo.StatStr -= item.STR;
        PlayerInfo.StatInt -= item.INT;
        PlayerInfo.StatLuk -= item.LUK;
        PlayerInfo.Speed -= item.Speed;
        PlayerInfo.Jump -= item.Jump;
    }
}