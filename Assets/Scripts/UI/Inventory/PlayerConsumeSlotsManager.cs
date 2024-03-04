using UnityEngine;

public class PlayerConsumeSlotsManager : MonoBehaviour
{
    private SlotUI[] _slots;
    private InventoryUI _inventoryUI;

    private void Awake()
    {
        _slots = GetComponentsInChildren<SlotUI>();
        _inventoryUI = GameManager.UIManager.GetUI<InventoryUI>();
        int i = 0;
        foreach(SlotUI slot in _slots)
        {
            slot.Init(i++);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_slots[0].HasItem())
            {
                UseItem(KeyCode.F1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (_slots[1].HasItem())
            {
                UseItem(KeyCode.F2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (_slots[2].HasItem())
            {
                UseItem(KeyCode.F3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            if (_slots[3].HasItem())
            {
                UseItem(KeyCode.F4);
            }
        }
    }

    private void UseItem(KeyCode code)
    {
        switch (code)
        {
            case KeyCode.F1:
                _inventoryUI.UseItem(_slots[0]);
                break;
            case KeyCode.F2:
                _inventoryUI.UseItem(_slots[1]);
                break;
            case KeyCode.F3:
                _inventoryUI.UseItem(_slots[2]);
                break;
            case KeyCode.F4:
                _inventoryUI.UseItem(_slots[3]);
                break;
        }
    }
}
