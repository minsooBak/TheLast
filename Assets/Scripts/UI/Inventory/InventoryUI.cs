using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private Transform _dropItemParent;
    [SerializeField] private TextMeshProUGUI _goldText;
    private SlotUI[] _slots;
    private Canvas _canvas;
    private ItemDataInfoUI _info;
    private Dictionary<int, int> _itemData;//index, ItemID
    public int Gold { get; private set; }

    [Header("MoveItem")]
    private SlotUI _curSlot;
    private Vector2 _startPos;
    private Vector2 _endPos;
    [SerializeField] private DropItemPopupUI _dropItemPopupUI;
    private int _dropIndex;
    private Transform _inventoryTransform;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _inventoryTransform = _slotParent.parent;
        var prefab = GameManager.ResourceManager.LoadPrefab("Prefabs/UI/Slot");
        _slots = new SlotUI[20];
        for(int i = 0; i < _slots.Length; ++i)
        {
            int indexX = i % 4;
            int indexY = i / 4;
            float x = (indexX * 140) + 40;
            float y = (indexY * -120) - 20;
            _slots[i] = Instantiate(prefab, _slotParent).GetComponent<SlotUI>();
            _slots[i].Init(i);
            var rect = _slots[i].GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(x, y);
        }

        if (Utility.IsExistsFile("InvenData.json"))
        {
            var data = Utility.LoadJsonFile<InvenData>("InvenData.json");
            _itemData = new(data.slotDatas.Count);
            GameManager.DataBases.TryGetDataBase(out ItemDataBase database);
            for (int i = 0; i < data.slotDatas.Count; ++i)
            {
                SlotData slotData = data.slotDatas[i];
                var item = database.GetData(slotData.id);
                _slots[slotData.index].SetItem(item);
                _slots[slotData.index].SetAmount(slotData.amount);
                _itemData.Add(slotData.index, slotData.id);
            }
        }
        else
        {
            _itemData = new();
        }

        //인벤토리 Active
        //transform.parent.GetComponent<PlayerInput>().PlayerActions.Inventory.canceled += _ => { _canvas.enabled = !_canvas.enabled; };
        _info = GameManager.UIManager.ShowUI<ItemDataInfoUI>();
        _info.Disable();
        _dropItemPopupUI.Disable();
    }

    public void AddItem(int id)
    {
        foreach(var pair in _itemData)
        {
            if(pair.Value == id && !_slots[pair.Key].IsMaxAmount)
            {
                _slots[pair.Key].AddAmount();
                return;
            }
        }
        GameManager.DataBases.TryGetDataBase(out ItemDataBase data);
        var item = data.GetData(id);
        if (_itemData.Count == _slots.Length)
        {
            Instantiate(item.DropItem, _dropItemParent);
            Debug.Log("Inventory Full");
            return;
        }
        for (int i = 0; i < _slots.Length; ++i)
        {
            if (!_slots[i].HasItem())
            {
                _slots[i].SetItem(item);
                _itemData.Add(i, item.ID);
                return;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (_itemData.Count == 0) return;

        InvenData data = new()
        {
            slotDatas = new(_itemData.Count)
        };
        foreach (var item in _itemData)
        {
            SlotData slotData = new()
            {
                id = item.Value,
                index = item.Key,
                amount = _slots[item.Key].Amount
            };
            data.slotDatas.Add(slotData);
        }

        Utility.SaveToJsonFile(data, "InvenData.json");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == null || !obj.TryGetComponent(out _curSlot)) return;

        if (!_curSlot.HasItem())
        {
            _curSlot = null;
            return;
        }
        _info.Disable();
        _curSlot.IconTransform.SetParent(_inventoryTransform);
        _startPos = _curSlot.IconTransform.position;
        _endPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_curSlot == null) return;
        _curSlot.IconTransform.position = _startPos + (eventData.position - _endPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_curSlot == null) return;

        Vector3 pos = (_curSlot.IconTransform as RectTransform).localPosition;
        Rect baseRect = (transform.GetChild(0) as RectTransform).rect;
        _curSlot.IconReset();

        if (pos.x < baseRect.xMin || pos.x > baseRect.xMax
            || pos.y < baseRect.yMin || pos.y > baseRect.yMax)
        {
            _dropIndex = _curSlot.Index;
            _dropItemPopupUI.Active();
            if (_curSlot.Amount == 1)
            {
                _dropItemPopupUI.Init("Itme Popup", "Are you sure you want to discard this?", DropItem);
            }
            else
            {
                _dropItemPopupUI.Init("Item Popup", "How many would you like to discard?", DropItems, _curSlot.Amount);
            }
        }
        else
        {
            if (!eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out SlotUI endSlot)) return;

            ItemEntity item = _curSlot.Item;
            int amount = _curSlot.Amount;

            _curSlot.SetItem(endSlot.Item);
            _curSlot.SetAmount(endSlot.Amount);
            endSlot.SetItem(item);
            endSlot.SetAmount(amount);

            if (_itemData.ContainsKey(endSlot.Index))
            {
                int ID = _itemData[endSlot.Index];
                _itemData[endSlot.Index] = _itemData[_curSlot.Index];
                _itemData[_curSlot.Index] = ID;
            }
            else
            {
                int id = _itemData[_curSlot.Index];
                _itemData.Remove(_curSlot.Index);
                _itemData.Add(endSlot.Index, id);
            }
        }
    }

    private void DropItem()
    {
        _slots[_dropIndex].SetItem(null);
        _itemData.Remove(_dropIndex);
    }

    private void DropItems(int amount)
    {
        int result = _slots[_dropIndex].Amount - amount;
        if(result == 0 || result == 1)
        {
            _slots[_dropIndex].SetAmount(1);
            DropItem();
        }
        else
        {
            _slots[_dropIndex].SetAmount(result);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == null || !obj.TryGetComponent(out SlotUI slot) || slot.Item == null) return;
        _info.Init(slot.Item);
        _info.Active();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _info.Disable();
    }

    private class InvenData
    {
        public List<SlotData> slotDatas;
    }

    [System.Serializable]
    private class SlotData
    {
        public int index;
        public int id;
        public int amount;
    }
}
