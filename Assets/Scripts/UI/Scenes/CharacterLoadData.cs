using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterLoadData
{
    public UserDataList userDataList;
    public Dictionary<int, UserData> loadedUserData;

    public void Init()
    {
        loadedUserData = new(3);
        //for (int i = 0; i < 3; i++)
        //{
        //    loadedUserData[i+1] = null;
        //}
        userDataList = new UserDataList();
        //Utility.LoadJsonFile<UserDataList>("UsersData.json");
        LoadUserData();

    }
    [ContextMenu("To Json Data")]
    public void SaveUserData()
    {
        userDataList.user.Clear();
        foreach (KeyValuePair<int, UserData> pair in loadedUserData)
        {
            if (loadedUserData.ContainsKey(pair.Key))
                userDataList.user.Add(pair.Value);
        }
        string jsonData = JsonUtility.ToJson(userDataList, true);
        string path = Path.Combine(Application.dataPath, $"UserData.json");
        File.WriteAllText(path, jsonData);
        Debug.Log("데이터 저장했음");
    }

    [ContextMenu("From Json Data")]
    private void LoadUserData()
    {
        string path = Path.Combine(Application.dataPath, $"UserData.json");
        if (!File.Exists(path))
        {
            Debug.Log("No File");
            //userDataList = new UserDataList();
            return;
        }
        string jsonData = File.ReadAllText(path);

        userDataList = JsonUtility.FromJson<UserDataList>(jsonData);
        for (int i = 0; i < userDataList.user.Count; i++)
        {
            if (userDataList.user[i]!= null)
            {
                loadedUserData.Remove(userDataList.user[i].characterSlot);
                loadedUserData.Add(userDataList.user[i].characterSlot, userDataList.user[i]);
            }
        }
        Debug.Log("데이터 불러왔음");
    }
}
[System.Serializable]
public class UserData
{
    public int characterSlot;
    public byte statusId;
    public string characterName;
    public int Level;
    public int exp;
    public int stageLv;
    //플레이어인벤토리,스킬업글,스텟업글
}
public class PlayData
{

}

[System.Serializable]
public class UserDataList
{
    public List<UserData> user = new List<UserData>();
}




