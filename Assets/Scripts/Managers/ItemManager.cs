using System.Collections.Generic;
using Enums;
using System;

[System.Serializable]
public class ItemManager
{
    private Dictionary<int, ItemEntity> _inventoryItemData = new();//index, ItemID
    private Dictionary<ItemType, ItemEntity> _equipItemData = new(Enum.GetValues(typeof(ItemType)).Length);
    private PlayerInfoManager _playerInfoManager;
    public Dictionary<int, ItemEntity> GetInventoryItemData() { return _inventoryItemData; }
    private InventoryUI inventoryUI;

    public void Init(InvenData invenData)
    {
        if (invenData == null) return;

        _inventoryItemData = new(invenData.slotDatas.Count);

        foreach (SlotData slot in invenData.slotDatas)
        {
            _inventoryItemData.Add(slot.index, slot.item);
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

    public void EquipItem(ItemEntity item)
    {
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
            SlotData slotData = new SlotData
            {
                index = item.Key,
                item = item.Value
            };
            data.slotDatas.Add(slotData);
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
    public ItemEntity item;
}