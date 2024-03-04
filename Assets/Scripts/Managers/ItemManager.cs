using System.Collections.Generic;
using Enums;
using System;
using UnityEngine.Assertions;

[System.Serializable]
public class ItemManager
{
    private Dictionary<int, SlotData> _inventoryItemData = new();//index, ItemID
    private Dictionary<ItemType, ItemEntity> _equipItemData = new(Enum.GetValues(typeof(ItemType)).Length);
    private PlayerInfoManager _playerInfoManager;
    public Dictionary<int, SlotData> GetInventoryItemData() { return _inventoryItemData; }
    public Dictionary<ItemType, ItemEntity> GetEquipItemData() { return _equipItemData; }

    private InventoryUI inventoryUI;
    private PlayerInfoUI playerInfoUI;

    public void Init(InvenData invenData)
    {
        if (invenData == null) return;

        _inventoryItemData = new(invenData.slotDatas.Count);

        foreach (SlotData slot in invenData.slotDatas)
        {
            _inventoryItemData.Add(slot.index, slot);
        }

        foreach(EquipData equip in invenData.equipDatas)
        {
            _equipItemData.Add(equip.type, equip.item);
        }
        _playerInfoManager = GameManager.PlayerManager.PlayerInfoManager;

    }

    public void AddItem(int id)
    {
        inventoryUI = inventoryUI != null ? inventoryUI : GameManager.UIManager.GetUI<InventoryUI>();

        inventoryUI.UpdateItem(id, _inventoryItemData);
    }

    public int GetConsumeAmount(int id)
    {
        int amount = 0;
        foreach(var item in _inventoryItemData)
        {
            if(item.Value.item.ID == id)
            {
                amount += item.Value.amount;
            }
        }
        return amount;
    }

    public int GetLastConsume(int id)
    {
        int index = -1;
        foreach(var item in _inventoryItemData)
        {
            if(item.Value.item.ID == id)
            {
                index = item.Key;
            }
        }
        Assert.IsTrue(index != -1);
        return index;
    }

    public void UnEquipItem(ItemEntity item)
    {
        inventoryUI = inventoryUI != null ? inventoryUI : GameManager.UIManager.GetUI<InventoryUI>();

        Assert.IsTrue(_equipItemData.ContainsKey(item.ItemType));
        inventoryUI.UpdateItem(_equipItemData[item.ItemType].ID, _inventoryItemData);
        _playerInfoManager.UnEquipItem(_equipItemData[item.ItemType]);
    }

    public void EquipItem(ItemEntity item)
    {
        playerInfoUI = playerInfoUI != null ? playerInfoUI : GameManager.UIManager.GetUI<PlayerInfoUI>();
        inventoryUI = inventoryUI != null ? inventoryUI : GameManager.UIManager.GetUI<InventoryUI>();

        if (_equipItemData.ContainsKey(item.ItemType))
        {
            inventoryUI.UpdateItem(_equipItemData[item.ItemType].ID, _inventoryItemData);
            _playerInfoManager.UnEquipItem(_equipItemData[item.ItemType]);
            _equipItemData[item.ItemType] = item;
            _playerInfoManager.EquipItem(item);
        }
        else
        {
            _equipItemData[item.ItemType] = item;
            _playerInfoManager.EquipItem(item);
        }
        playerInfoUI.EquipItem(item);
    }

    public InvenData GetData()
    {
        InvenData data = new InvenData
        {
            slotDatas = new(_inventoryItemData.Count),
            equipDatas = new(_equipItemData.Count)
        };
        foreach(var item in _inventoryItemData)
        {
            data.slotDatas.Add(item.Value);
        }
        foreach (var equip in _equipItemData)
        {
            EquipData equipData = new EquipData
            {
                type = equip.Key,
                item = equip.Value
            };
            data.equipDatas.Add(equipData);
        }
        return data;
    }

}
[System.Serializable]
public class InvenData
{
    public List<SlotData> slotDatas;
    public List<EquipData> equipDatas;
}

[System.Serializable]
public class EquipData
{
    public ItemType type;
    public ItemEntity item;
}

[System.Serializable]
public class SlotData
{
    public int index;
    public int amount;
    public ItemEntity item;
}