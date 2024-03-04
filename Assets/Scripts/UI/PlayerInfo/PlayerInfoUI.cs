using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Enums;
using UnityEngine.EventSystems;

public class PlayerInfoUI : UIBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private PlayerInfoManager _playerInfoManager;
    private PlayerInfo _playerInfo;
    [Header("PlayerInfo")]
    [SerializeField] private TextMeshProUGUI _playerNameText;
    [SerializeField] private TextMeshProUGUI _playerClassText;
    [SerializeField] private TextMeshProUGUI _playerLevelText;
    [SerializeField] private TextMeshProUGUI _playerExpText;
    [SerializeField] private TextMeshProUGUI _playerStatText;
    [Header("PlayerStatButton")]
    [SerializeField] private Button _strBtn;
    [SerializeField] private Button _intBtn;
    [SerializeField] private Button _lukBtn;
    [SerializeField] private TextMeshProUGUI _statPoint;
    [Header("Equip Item")]
    [SerializeField] private SlotUI _weaponUI;
    [SerializeField] private SlotUI _armorUI;

    private ItemDataInfoUI _info;
    [Header("Item Click")]
    private SlotUI _lateSlot;
    [SerializeField] private float _interval = 0.25f;
    private float _doubleClickedTime = -1f;

    private void Awake()
    {
        _playerInfoManager = GameManager.PlayerManager.PlayerInfoManager;
        _playerInfo = _playerInfoManager.PlayerInfo;

        _playerNameText.text = $"이름 : {_playerInfo.PlayerName}";
        _playerClassText.text = $"직업 : {_playerInfo.Job}";
        UpdateText();

        _strBtn.onClick.AddListener(() => _playerInfoManager.StrUp());
        _intBtn.onClick.AddListener(() => _playerInfoManager.IntUp());
        _lukBtn.onClick.AddListener(() => _playerInfoManager.LukUp());

        _info = GameManager.UIManager.GetUI<ItemDataInfoUI>();

        Disable();
    }

    private void LateUpdate()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _playerLevelText.text = $"레벨 : {_playerInfo.Level}";
        _playerExpText.text = $"{_playerInfo.Exp:N0} / {_playerInfoManager.GetMaxExp():N0}";
        _playerStatText.text =
            $"HP : {_playerInfo.Hp} / {_playerInfo.MaxHp}\nMP : {_playerInfo.Mp} / {_playerInfo.MaxMp}\n" +
            $"STR : {_playerInfo.StatStr}\nINT : {_playerInfo.StatInt}\nLuk : {_playerInfo.StatLuk}\nJump : {_playerInfo.Jump}%\nSpeed : {_playerInfo.Speed}%";
        _statPoint.text = $"StatPoint : {_playerInfo.StatPoint}";
    }

    public void EquipItem(ItemEntity item)
    {
        switch(item.ItemType)
        {
            case ItemType.Weapon:
                _weaponUI.SetItem(item);
                break;
            case ItemType.Armor:
                _armorUI.SetItem(item);
                break;
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
            if ((Time.time - _doubleClickedTime) < _interval && slot == _lateSlot)
            {
                _doubleClickedTime = -1f;

                GameManager.PlayerManager.ItemManager.UnEquipItem(slot.Item);
                slot.SetItem(null);
            }
            else
            {
                _lateSlot = slot;
                _doubleClickedTime = Time.time;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GameManager.PlayerManager.ItemManager.UnEquipItem(slot.Item);
            slot.SetItem(null);
        }
    }
}
