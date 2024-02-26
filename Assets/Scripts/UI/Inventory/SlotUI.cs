using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _highlight;

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
        if (icon != null)
            _icon.enabled = true;
        else
            _icon.enabled = false;
    }

    public void SetAmount(int amount)
    {
        if (amount == 0)
        {
            _amountText.enabled = false;
            return;
        }

        if (!_amountText.enabled) _amountText.enabled = true;

        _amountText.text = amount.ToString();
    }

    public void SetColor(Color color)
    {
        _highlight.color = color;
    }
}
