using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelectData : MonoBehaviour
{
    private PlayerStatusDB statusDB;
    private PlayerStatusInfo statusInfo;
    public List<PlayerStatusInfo> defaultDataList;
    private CharacterSelectUI selectUI;

    private void Awake()
    {
        statusDB = new PlayerStatusDB();
        selectUI = GetComponent<CharacterSelectUI>();
    }
    private void Start()
    {
        defaultDataList = new List<PlayerStatusInfo>();
        CreateCharacterStatus(1);
        CreateCharacterStatus(2);
    }
    private void CreateCharacterStatus(byte _id)
    {
        statusInfo = statusDB.GetData(_id);
        defaultDataList.Add(statusDB.GetData(_id));
    }
}
