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
    [SerializeField] private Button _attackBtn;
    [SerializeField] private Transform slotParent;
    [SerializeField] private SkillSlotUI[] _slots;
    [SerializeField] private ItemDataInfoUI _infoUI;
    [SerializeField] private PlayerSkillHandler _handler;

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
        _handler = GameObject.Find("Player").GetComponent<PlayerSkillHandler>();
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

        _infoUI.transform.SetAsLastSibling();
        _attackBtn.onClick.Invoke();
        Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
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

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out SkillSlotUI endSlot))
        {
            SkillSlot();
            endSlot.SetSkill(_curSlot.Skill, _curSlot.Index);
        }
    }
    private void SkillSlot()
    {
        Debug.Log(_slots[0].Skill._id);
        _handler.SkillSoltChange(_slots[0].Skill._id, 0);
        _handler.SkillSoltChange(_slots[1].Skill._id, 1);
        _handler.SkillSoltChange(_slots[2].Skill._id, 2);
        _handler.SkillSoltChange(_slots[3].Skill._id, 3);
    }
}
