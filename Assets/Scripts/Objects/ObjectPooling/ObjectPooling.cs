using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount;
    [SerializeField] private Queue<GameObject> _objects;

    public void Awake()
    {
        _objects = new Queue<GameObject>(amount);

        for(int i = 0; i < amount; ++i)
        {
            var obj = Instantiate(prefab, transform);
            obj.AddComponent<ObjectPool>();
            _objects.Enqueue(obj);
        }
        Assert.IsTrue(_objects.Count == amount);
    }
    public GameObject GetObject()
    {
        var obj = _objects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public GameObject GetObject(Vector3 position)
    {
        var obj = _objects.Dequeue();
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _objects.Enqueue(obj);
    }
}
