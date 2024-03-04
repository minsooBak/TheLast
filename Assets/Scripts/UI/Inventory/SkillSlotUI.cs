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

    private void Awake()
    {
        _rectTransform = _icon.GetComponent<RectTransform>();
    }

    public void Init(Action upBtn)
    {
        _upBtn.onClick.AddListener(() => upBtn());
    }

    public void IconReset()
    {
        _icon.transform.SetParent(transform);
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    public void SetSkill(PlayerSkillInfo skill, int index)
    {
        Index = index;
        _skill = skill;
        if (_skill != null)
        {
            _icon.enabled = true;
            //_icon.sprite = _skill.Sprite;
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
}
