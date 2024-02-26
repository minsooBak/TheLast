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
        statusDB = new PlayerStatusDB();
        levelDB = new PlayerLevelDB();

        player = GetComponent<Player>();
    }
    private void Start()
    {
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
        player.playerInfo.Adef = statusInfo._adef;
        player.playerInfo.Mdef = statusInfo._mdef;
        player.playerInfo.StatStr = statusInfo._str;
        player.playerInfo.StatInt = statusInfo._int;
        player.playerInfo.StatLuk = statusInfo._luk;
        player.playerInfo.Jump = statusInfo._jump;
        player.playerInfo.Speed = statusInfo._speed;
    }
    private void UpdateLevel()
    {
    }
}
