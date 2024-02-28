using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
public class PlayerInfoHandler : MonoBehaviour
{
    private PlayerStatusDB statusDB;
    private PlayerLevelDB levelDB;

    private PlayerStatusInfo statusInfo;
    private PlayerLevelInfo levelInfo;
    private Player player;

    private byte _id = 1;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        statusDB = new PlayerStatusDB();
        levelDB = new PlayerLevelDB(_id);
        CreateCharacterStatus(_id);
    }
    private void CreateCharacterStatus(byte _id)
    {
        statusInfo = statusDB.GetData(_id);

        player.playerInfo.Job = statusInfo._job;
        player.playerInfo.Hp = statusInfo._hp;
        player.playerInfo.MaxHp = statusInfo._hp;
        player.playerInfo.Mp = statusInfo._mp;
        player.playerInfo.MaxMp = statusInfo._mp;
        player.playerInfo.ADef = statusInfo._adef;
        player.playerInfo.MDef = statusInfo._mdef;
        player.playerInfo.StatStr = statusInfo._str;
        player.playerInfo.StatInt = statusInfo._int;
        player.playerInfo.StatLuk = statusInfo._luk;
        player.playerInfo.Jump = statusInfo._jump;
        player.playerInfo.Speed = statusInfo._speed;
    }
    private void UpdateLevel(float exp)
    {
        player.playerInfo.Exp += exp;

        for (int i = 1; i < levelDB.GetLevelCount() - 1; i++)
        {
            if (levelDB.GetData(i)._exp <= player.playerInfo.Exp
                && player.playerInfo.Exp > levelDB.GetData(i + 1)._exp)
            {
                if (player.playerInfo.Level != (short)i)
                {
                    player.playerInfo.Level = (short)i;
                    LevelUp();
                }
            }
        }
    }
    private void LevelUp()
    {
        levelInfo = levelDB.GetData(player.playerInfo.Level - 1);

        player.playerInfo.MaxHp -= levelInfo._maxHp;
        player.playerInfo.MaxMp -= levelInfo._maxMp;
        player.playerInfo.ADef -= levelInfo._adef;
        player.playerInfo.MDef -= levelInfo._mdef;
        player.playerInfo.StatStr -= levelInfo._str;
        player.playerInfo.StatInt -= levelInfo._int;
        player.playerInfo.StatLuk -= levelInfo._luk;

        levelInfo = levelDB.GetData(player.playerInfo.Level);

        player.playerInfo.MaxHp += levelInfo._maxHp;
        player.playerInfo.MaxMp += levelInfo._maxMp;
        player.playerInfo.ADef += levelInfo._adef;
        player.playerInfo.MDef += levelInfo._mdef;
        player.playerInfo.StatStr += levelInfo._str;
        player.playerInfo.StatInt += levelInfo._int;
        player.playerInfo.StatLuk += levelInfo._luk;
    }
}
