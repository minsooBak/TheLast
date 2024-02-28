using UnityEngine;
using UnityEngine.Assertions;

public class ResourceManager : ScriptableObject
{
    public GameObject Instantiate(string path)
    {
        var obj = Resources.Load<GameObject>(path);
        Assert.IsNotNull(obj, $"Prefab Load Faill : {path}");
        GameObject result = Instantiate(obj);
        return result;
    }
    public GameObject Instantiate(string path, Transform parent) 
    {
        var obj = Resources.Load<GameObject>(path);
        Assert.IsNotNull(obj, $"Prefab Load Faill : {path}");
        GameObject result = Instantiate(obj, parent);
        return result;
    }
    public GameObject LoadPrefab(string path)
    {
        var obj = Resources.Load<GameObject>(path);
        Assert.IsNotNull(obj, $"Prefab Load Faill : {path}");
        return obj;
    }

    public void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }
}
