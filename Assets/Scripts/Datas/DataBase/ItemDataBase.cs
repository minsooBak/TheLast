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

    public int Amount { get; set; } = 1;
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
            if (DropItem == null)
                DropItem = Resources.Load<GameObject>($"Prefab/{ItemPath}");
            return DropItem;
        }
        private set { DropItem = value; }
    }
}
