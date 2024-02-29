using UnityEngine.Assertions;
using UnityEngine;
using System.IO;

public static class Utility
{
    private readonly static string _dataPath = Application.dataPath + "/Data/";

    public static void SaveToJsonFile<T>(T data, string path)
    {
        File.WriteAllText(_dataPath + path, JsonUtility.ToJson(data));
        Debug.Log($"Save file : {_dataPath + path}");
    }

    public static T LoadJsonFile<T>(string path)
    {
        Assert.IsTrue(File.Exists(_dataPath + path), $"Exists Json File is Null : {_dataPath}{path}");

        string sr = File.ReadAllText(_dataPath + path);
        return JsonUtility.FromJson<T>(sr);
    }

    public static string[,] LoadCSVFile(string path)
    {
        Assert.IsTrue(File.Exists(_dataPath + path), $"Exists CSV File is Null : {_dataPath + path}");
        TextAsset csvData = Resources.Load<TextAsset>(_dataPath + path);

        string[] lines = csvData.text.Split('\n');

        string[] tempArr = lines[0].Split(',');
        string[,] data = new string[lines.Length, tempArr.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] col = lines[i].Split(',');
            for (int j = 0; j < col.Length; i++)
            {
                data[i, j] = col[j];
            }
        }
        Assert.IsTrue(data.GetLength(0) > 0, $"CSV Data is Null : {lines.Length}");

        return data;
    }

    public static bool IsExistsFile(string path)
    {
        return File.Exists(_dataPath + path);
    }
    public static int GetHashWithString(string path)
    {
        return path.GetHashCode();
    }
    public static int GetHashWithTag(GameObject path)
    {
        return path.tag.GetHashCode();
    }
}