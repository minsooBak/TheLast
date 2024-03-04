using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Button _upBtn;

    private RectTransform _rectTransform;
    private PlayerSkillInfo _skill;

    public int Index { get; private set; } 

    public PlayerSkillInfo Skill { get { return _skill; } }
    public Transform IconTransform { get { return _icon.transform; } }

    private PlayerInfo _playerInfo;
    private PlayerSkill _playerSkill;
    private PlayerSkillDB _skillDB;

    private void Awake()
    {
        _rectTransform = _icon.GetComponent<RectTransform>();

    }

    public void Init(PlayerInfo info, PlayerSkill skill, PlayerSkillDB skillDB)
    {
        _playerInfo = info;
        _playerSkill = skill;
        _skillDB = skillDB;
        _upBtn.onClick.AddListener(SkillUp);
    }

    public void IconReset()
    {
        _icon.transform.SetParent(transform);
        _icon.transform.SetAsFirstSibling();
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    public void SetSkill(PlayerSkillInfo skill, int index)
    {
        Index = index;
        _skill = skill;
        if (_skill != null)
        {
            _icon.enabled = true;
            _icon.sprite = _skill.Sprite;
            SetAmount(skill._upgrade);
        }
        else
        {
            _icon.enabled = false;
            _icon.sprite = null;
            _amountText.enabled = false;
        }
    }

    public void SetAmount(int amount)
    {
        Assert.IsNotNull(_skill);

        if (amount <= 0)
        {
            _amountText.enabled = false;
            return;
        }

        if (!_amountText.enabled) _amountText.enabled = true;

        _amountText.text = amount.ToString();
    }

    private void SkillUp()
    {
        switch (Index + 1)
        {
            case 1:
                Debug.Log("1");
                if (_playerSkill.playerSkillInfo[101] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(101)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[101] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(101)._skillPoint;
                }
                break;
            case 2:
                Debug.Log("2");
                if (_playerSkill.playerSkillInfo[102] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(102)._skillPoint
                    && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[102] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(102)._skillPoint;
                }
                break;
            case 3:
                Debug.Log("3");
                if (_playerSkill.playerSkillInfo[103] > 3
                    && _playerInfo.SkillPoint <= _skillDB.GetData(103)._skillPoint
                    && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[103] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(103)._skillPoint;
                }
                break;
            case 4:
                Debug.Log("4");
                if (_playerSkill.playerSkillInfo[104] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(104)._skillPoint
                && _playerSkill.playerSkillInfo[101] >= 1)
                {
                    _playerSkill.playerSkillInfo[104] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(104)._skillPoint;
                }
                break;
            case 5:
                Debug.Log("5");
                if (_playerSkill.playerSkillInfo[105] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(105)._skillPoint
                && _playerSkill.playerSkillInfo[104] >= 1)
                {
                    _playerSkill.playerSkillInfo[105] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(105)._skillPoint;
                }
                break;
            case 6:
                Debug.Log("6");
                if (_playerSkill.playerSkillInfo[106] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(106)._skillPoint
                && _playerSkill.playerSkillInfo[102] >= 1)
                {
                    _playerSkill.playerSkillInfo[106] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(106)._skillPoint;
                }
                break;
            case 7:
                Debug.Log("7");
                if (_playerSkill.playerSkillInfo[107] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(107)._skillPoint
                && _playerSkill.playerSkillInfo[103] >= 1)
                {
                    _playerSkill.playerSkillInfo[107] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(107)._skillPoint;
                }
                break;
            case 8:
                Debug.Log("8");
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
                Debug.Log("9");
                if (_playerSkill.playerSkillInfo[201] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(201)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[201] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(201)._skillPoint;
                }
                break;
            case 10:
                Debug.Log("10");
                if (_playerSkill.playerSkillInfo[202] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(202)._skillPoint
                && _playerSkill.playerSkillInfo[201] >= 1)
                {
                    _playerSkill.playerSkillInfo[202] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(202)._skillPoint;
                }
                break;
            case 11:
                Debug.Log("11");
                if (_playerSkill.playerSkillInfo[203] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(203)._skillPoint
                && _playerSkill.playerSkillInfo[202] >= 1)
                {
                    _playerSkill.playerSkillInfo[203] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(203)._skillPoint;
                }
                break;
            case 12:
                Debug.Log("12");
                if (_playerSkill.playerSkillInfo[204] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(204)._skillPoint)
                {
                    _playerSkill.playerSkillInfo[204] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(204)._skillPoint;
                }
                break;
            case 13:
                Debug.Log("13");
                if (_playerSkill.playerSkillInfo[205] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(205)._skillPoint
                && _playerSkill.playerSkillInfo[204] >= 1)
                {
                    _playerSkill.playerSkillInfo[205] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(205)._skillPoint;
                }
                break;
            case 14:
                Debug.Log("14");
                if (_playerSkill.playerSkillInfo[206] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(206)._skillPoint
                && _playerSkill.playerSkillInfo[203] >= 1)
                {
                    _playerSkill.playerSkillInfo[206] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(206)._skillPoint;
                }
                break;
            case 15:
                Debug.Log("15");
                if (_playerSkill.playerSkillInfo[207] > 3
                && _playerInfo.SkillPoint <= _skillDB.GetData(207)._skillPoint
                && _playerSkill.playerSkillInfo[203] >= 1)
                {
                    _playerSkill.playerSkillInfo[207] += 1;
                    _playerInfo.SkillPoint -= _skillDB.GetData(207)._skillPoint;
                }
                break;
            case 16:
                Debug.Log("16");
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
