using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterDefaultData
{
    private PlayerStatusDB statusDB;
    private PlayerStatusInfo statusInfo;
    public List<PlayerStatusInfo> defaultData;

    public void Init()
    {
        statusDB = new PlayerStatusDB();
        defaultData = new List<PlayerStatusInfo>();
        CreateCharacterStatus(1);
        CreateCharacterStatus(2);
    }
    private void CreateCharacterStatus(byte _id)
    {
        statusInfo = statusDB.GetData(_id);
        defaultData.Add(statusDB.GetData(_id));
    }
}
