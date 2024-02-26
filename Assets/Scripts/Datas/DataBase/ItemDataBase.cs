using UnityEngine;

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

    public Sprite Sprite
    {
        get
        {
            if (Sprite == null)
                Sprite = Resources.Load<Sprite>($"Sprite/{ItemPath}");
            return Sprite;
        }
        private set { Sprite = value; }
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

public enum ItemType
{
    Weapon,
    Armor,
}