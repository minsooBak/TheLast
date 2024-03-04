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
            _slots[index].Init(SkillUp);
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
    private void SkillUp()
    {
        var slot = GetComponent<SkillSlotUI>();

        switch (slot.Index + 1)
        {
            case 1:
                if (_playerSkill.playerSkillInfo[101] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(101)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[101] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(101)._skillPoint;
                }
                break;
            case 2:
                if (_playerSkill.playerSkillInfo[102] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(102)._skillPoint
                    && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[102] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(102)._skillPoint;
                }
                break;
            case 3:
                if (_playerSkill.playerSkillInfo[103] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(103)._skillPoint
                    && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[103] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(103)._skillPoint;
                }
                break;
            case 4:
                if (_playerSkill.playerSkillInfo[104] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(104)._skillPoint
                && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[104] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(104)._skillPoint;
                }
                break;
            case 5:
                if (_playerSkill.playerSkillInfo[105] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(105)._skillPoint
                && _playerSkill.playerSkillInfo[104] >= 1)
                {
                    _playerSkill.playerSkillInfo[105] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(105)._skillPoint;
                }
                break;
            case 6:
                if (_playerSkill.playerSkillInfo[106] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(106)._skillPoint
                && _playerSkill.playerSkillInfo[102] >= 1)
                {
                    _playerSkill.playerSkillInfo[106] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(106)._skillPoint;
                }
                break;
            case 7:
                if (_playerSkill.playerSkillInfo[107] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(107)._skillPoint
                && _playerSkill.playerSkillInfo[103] >= 1)
                {
                    _playerSkill.playerSkillInfo[107] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(107)._skillPoint;
                }
                break;
            case 8:
                if (_playerSkill.playerSkillInfo[108] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(108)._skillPoint
                && _playerSkill.playerSkillInfo[105] >= 1
                && _playerSkill.playerSkillInfo[106] >= 1
                && _playerSkill.playerSkillInfo[107] >= 1)
                {
                    _playerSkill.playerSkillInfo[108] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(108)._skillPoint;
                }
                break;
            case 9:
                if (_playerSkill.playerSkillInfo[201] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(201)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[201] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(201)._skillPoint;
                }
                break;
            case 10:
                if (_playerSkill.playerSkillInfo[202] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(202)._skillPoint
                && _playerSkill.playerSkillInfo[201] >= 1)
                {
                    _playerSkill.playerSkillInfo[202] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(202)._skillPoint;
                }
                break;
            case 11:
                if (_playerSkill.playerSkillInfo[203] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(203)._skillPoint
                && _playerSkill.playerSkillInfo[202] >= 1)
                {
                    _playerSkill.playerSkillInfo[203] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(203)._skillPoint;
                }
                break;
            case 12:
                if (_playerSkill.playerSkillInfo[204] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(204)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[204] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(204)._skillPoint;
                }
                break;
            case 13:
                if (_playerSkill.playerSkillInfo[205] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(205)._skillPoint
                && _playerSkill.playerSkillInfo[204] >= 1)
                {
                    _playerSkill.playerSkillInfo[205] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(205)._skillPoint;
                }
                break;
            case 14:
                if (_playerSkill.playerSkillInfo[206] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(206)._skillPoint
                && _playerSkill.playerSkillInfo[203] >= 1)
                {
                    _playerSkill.playerSkillInfo[206] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(206)._skillPoint;
                }
                break;
            case 15:
                if (_playerSkill.playerSkillInfo[207] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(207)._skillPoint
                && _playerSkill.playerSkillInfo[203] >= 1)
                {
                    _playerSkill.playerSkillInfo[207] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(207)._skillPoint;
                }
                break;
            case 16:
                if (_playerSkill.playerSkillInfo[208] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(208)._skillPoint
                && _playerSkill.playerSkillInfo[205] >= 1)
                {
                    _playerSkill.playerSkillInfo[208] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(208)._skillPoint;
                }
                break;
        }

    }
}
