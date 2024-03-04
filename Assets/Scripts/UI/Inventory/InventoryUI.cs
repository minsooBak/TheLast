using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : UIBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Transform _slotParent;
    [SerializeField] private Transform _dropItemParent;
    [SerializeField] private TextMeshProUGUI _goldText;
    private SlotUI[] _slots;
    private Canvas _canvas;
    private ItemDataInfoUI _info;
    private ItemManager _itemManager;
    public int Gold { get; private set; }

    [Header("MoveItem")]
    private SlotUI _curSlot;
    private Vector2 _startPos;
    private Vector2 _endPos;
    [SerializeField] private DropItemPopupUI _dropItemPopupUI;
    private int _dropIndex;
    private Transform _inventoryTransform;

    [Header("Item Click")]
    private SlotUI _lateSlot;
    [SerializeField] private float _interval = 0.25f;
    private float _doubleClickedTime = -1f;

    private void Awake()
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
        _itemManager = GameManager.PlayerManager.ItemManager;
        var slotDatas = _itemManager.GetInventoryItemData();
        if (slotDatas.Count != 0)
        {
            foreach(var data in slotDatas)
            {
                _slots[data.Key].SetItem(data.Value);
            }
        }
        //인벤토리 Active
        //transform.parent.GetComponent<PlayerInput>().PlayerActions.Inventory.canceled += _ => { _canvas.enabled = !_canvas.enabled; };
        _info = GameManager.UIManager.GetUI<ItemDataInfoUI>();
        _info.Disable();
        _dropItemPopupUI.Disable();

        Disable();
    }

    public void UpdateItem(int id, in Dictionary<int, ItemEntity> itemData)
    {
        foreach (var pair in itemData)
        {
            if(pair.Value.ID == id && !_slots[pair.Key].IsMaxAmount)
            {
                _slots[pair.Key].AddAmount();
                return;
            }
        }
        GameManager.DataBases.TryGetDataBase(out ItemDataBase data);
        var item = data.GetData(id);

        if (itemData.Count == _slots.Length)
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
                itemData.Add(i, item);
                return;
            }
        }
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

            if (endSlot.transform.parent.parent.parent.TryGetComponent(out InventoryUI _))
            {
                DropInventory(endSlot);
            }
            //장비 장착
            //소모품 슬롯 배치
            
        }
    }

    private void DropInventory(SlotUI endSlot)
    {
        var data = _itemManager.GetInventoryItemData();
        ItemEntity item = _curSlot.Item;
        int amount = _curSlot.Amount;

        _curSlot.SetItem(endSlot.Item);
        if (_curSlot.HasItem()) _curSlot.SetAmount(endSlot.Amount);
        endSlot.SetItem(item);
        endSlot.SetAmount(amount);

        if (data.ContainsKey(endSlot.Index))
        {
            data[endSlot.Index] = data[_curSlot.Index];
            data[_curSlot.Index] = item;
        }
        else
        {
            data.Remove(_curSlot.Index);
            data.Add(endSlot.Index, item);
        }
    }

    private void DropItem()
    {
        _slots[_dropIndex].SetItem(null);
        _itemManager.GetInventoryItemData().Remove(_dropIndex);
    }

    private void DropItems(int amount)
    {
        int result = _slots[_dropIndex].Amount - amount;
        if(result == 0)
        {
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

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == null || !obj.TryGetComponent(out SlotUI slot) || slot.Item == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if((Time.time - _doubleClickedTime) < _interval && slot == _lateSlot)
            {
                _doubleClickedTime = -1f;

                //Item Use
                UseItem(slot);
                Debug.Log("Item Left Use");
            }
            else
            {
                _lateSlot = slot;
                _doubleClickedTime = Time.time;
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            //Item Use
            UseItem(slot);
            Debug.Log("Item Right Use");
        }
    }

    private void UseItem(SlotUI slot)
    {
        switch(slot.Item.ItemType)
        {
            case Enums.ItemType.Weapon:
            case Enums.ItemType.Armor:
                {
                    ItemEntity item = slot.Item;
                    slot.SetItem(null);
                    _itemManager.GetInventoryItemData().Remove(slot.Index);
                    _itemManager.EquipItem(item);
                }
                break;
            case Enums.ItemType.Consume:
                {
                    //포션 사용
                    UseConsume(slot.Item);
                    if (slot.Amount == 1)
                    {
                        _itemManager.GetInventoryItemData().Remove(slot.Index);
                        slot.SetItem(null);
                    }
                    else
                        slot.SetAmount(slot.Amount - 1);
                }
                break;
        }
    }

    private void UseConsume(ItemEntity item)
    {
        if(item.HP > 0 || item.MP > 0)
        {
            //HP 및 MP 회복
            Debug.Log($"{item.HP} 회복");
            Debug.Log($"{item.MP} 회복");
        }
        else //TODO : 영구적으로 오르는것 or 일정 시간버프가 있겠지만 그건 추후 논의
        {
            //버프 or 디버프 생성
        }
    }
}
