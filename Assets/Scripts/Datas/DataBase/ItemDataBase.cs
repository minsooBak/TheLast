using UnityEngine;
using Enums;

public class ItemDataBase : DataBase<ItemEntity>
{
    protected override void Load()
    {
        var data = Resources.Load<ItemData>("Data/ItemData");
        _data = new(data.Entities.Count);
        foreach(var item in data.Entities)
        {
            _data.Add((item.ID), item);
        }
    }

    public GameObject GetRandomItem()
    {
        int r = Random.Range(0, _data.Count);
        int index = 0;
        foreach(var data in _data)
        {
            if(index == r)
            {
                return data.Value.DropItem;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ItemEntity
{
    public string Name;
    public int ID;
    public ItemType ItemType;
    public int HP;
    public int MP;
    public int ADEF;
    public int MDEF;
    public int STR;
    public int INT;
    public int LUK;
    public int Jump;
    public int Speed;
    public string Description;
    public string ItemPath;
    public int MaxAmount;

    private Sprite _sprite;
    private GameObject _dropItem;

    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
                _sprite = Resources.Load<Sprite>($"Icon/Item/{ItemPath}");
            return _sprite;
        }
    }

    public GameObject DropItem
    {
        get
        {
            if (_dropItem == null)
                _dropItem = Resources.Load<GameObject>($"Prefab/Item/{ItemPath}");
            return _dropItem;
        }
    }
}
