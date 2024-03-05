using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkillUI : UIBase, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PlayerSkill _playerSkill;
    private PlayerSkillDB _skillDB;
    private PlayerInfo _playerInfo;

    [SerializeField] private TMPro.TextMeshProUGUI _headerText;
    [SerializeField] private TMPro.TextMeshProUGUI _skillPoint;
    [SerializeField] private Button _attackBtn;
    [SerializeField] private Transform slotParent;
    [SerializeField] private SkillSlotUI[] _slots;
    [SerializeField] private ItemDataInfoUI _infoUI;

    [Header("MoveItem")]
    private SkillSlotUI _curSlot;
    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Awake()
    {
        var skillManager = GameManager.PlayerManager.SkillManager;
        _playerSkill = skillManager.PlayerSkill;
        _skillDB = skillManager.skillData;
        _playerInfo = GameManager.PlayerManager.PlayerInfoManager.PlayerInfo;
        _headerText.text = GameManager.PlayerManager.PlayerInfoManager.userData.statusId == 1 ? "마법사" : "오크전사";
        _slots = slotParent.GetComponentsInChildren<SkillSlotUI>();
        _infoUI = GameManager.UIManager.GetUI<ItemDataInfoUI>();
        int index = 0;
        foreach (var skill in _playerSkill.playerSkillInfo)
        {
            var data = _skillDB.GetData(skill.Key);
            data._upgrade = skill.Value;
            _slots[index].Init(_playerInfo, _playerSkill, _skillDB);
            _slots[index].SetSkill(data, index++);
        }
        UpdateSkillPoint();

        _infoUI.transform.SetAsLastSibling();
        _attackBtn.onClick.Invoke();
        Disable();
    }

    public void UpdateSkillPoint()
    {
        _skillPoint.text = $"Skill Point : {_playerInfo.SkillPoint}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        UpdateSkillPoint();
        if (obj == null || !obj.TryGetComponent(out SkillSlotUI slot) || slot.Skill == null) return;
        _infoUI.Init(slot.Skill);
        _infoUI.Active();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _infoUI.Disable();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        if (obj == null || !obj.TryGetComponent(out _curSlot)) return;

        if (_curSlot.Skill._upgrade == 0)
        {
            _curSlot = null;
            return;
        }

        _infoUI.Disable();
        _curSlot.IconTransform.SetParent(slotParent);
        _startPos = _curSlot.transform.position;
        _endPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_curSlot == null) return;
        _curSlot.IconTransform.position = _startPos + (eventData.position - _endPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_curSlot == null) return;
        _curSlot.IconReset();
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;

        if (obj != null && obj.TryGetComponent(out SkillSlotUI endSlot) && endSlot.transform.parent.name == "PlayerSkillSlots")
        {
            endSlot.SetSkill(_curSlot.Skill, _curSlot.Index);
        }
    }
}
